import React, { Component } from 'react'
import './App.css'
import Header from './components/Header'
import ProductGrid from './components/ProductGrid'
import ShoppingCartDrawer from './components/Drawer'
import Modal from './components/Modal'

class App extends Component {
	state = {
		products: [],
		cart: { products: [] },
		drawerOpen: false,
		totalPrice: 0,
		modalOpen: false
	}

	componentDidMount() {
		this.fetchProducts()
	}

	toggleDrawer = id => {
		this.setState({
			drawerOpen: !this.state.drawerOpen
		})
		!this.state.drawerOpen && this.fetchCart(2)
	}

	toggleModal = () => {
		this.setState({ modalOpen: !this.state.modalOpen })
		//console.log(this.state.cart)
	}

	fetchCart = id => {
		const api = `https://localhost:5001/api/cart/${id}`
		fetch(api)
			.then(res => res.json())
			.then(data =>
				this.setState(
					{
						cart: data
					},
					() => {
						this.updateTotal()
					}
				)
			)
	}

	updateTotal = () => {
		let totalPrice = this.state.cart.products
			.map(item => item.price)
			.reduce((total, item) => total + item)
		this.setState({
			totalPrice: totalPrice
		})
	}

	fetchProducts = () => {
		const api = 'https://localhost:5001/api/product'
		fetch(api)
			.then(res => res.json())
			.then(data =>
				this.setState({
					products: data.value
				})
			)
	}

	render() {
		return (
			<div className="App">
				<Header toggleDrawer={this.toggleDrawer} />
				<ShoppingCartDrawer
					toggleDrawer={this.toggleDrawer}
					drawerOpen={this.state.drawerOpen}
					cart={this.state.cart}
					totalPrice={this.state.totalPrice}
					toggleModal={this.toggleModal}
				/>
				<ProductGrid products={this.state.products} />
				<Modal
					modalOpen={this.state.modalOpen}
					toggleModal={this.toggleModal}
					cart={this.state.cart}
				/>
			</div>
		)
	}
}

export default App
