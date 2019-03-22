import React, { Component } from 'react'
import './App.css'
import Header from './components/Header'
import ProductGrid from './components/ProductGrid'
import ShoppingCartDrawer from './components/Drawer'
import Modal from './components/Modal'
import Snackbar from './components/Snackbar'

class App extends Component {
	state = {
		products: [],
		cart: { products: [] },
		drawerOpen: false,
		totalPrice: 0,
		modalOpen: false,
		snackbar: {
			isOpen: false,
			type: ''
		}
	}

	componentDidMount() {
		this.fetchProducts()
	}

	toggleDrawer = id => {
		this.setState({
			drawerOpen: !this.state.drawerOpen
		})
		!this.state.drawerOpen && this.fetchCart(5)
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
						this.saveToLocalStorage()
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

	saveToLocalStorage = () => {
		localStorage.setItem('cart', JSON.stringify(this.state.cart))
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

	toggleSnackbar = () => {}

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
