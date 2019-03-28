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
		activeCart: '',
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
		this.setState(
			{
				activeCart: JSON.parse(localStorage.getItem('currentCartId')),
				drawerOpen: !this.state.drawerOpen
			},
			() => {
				this.fetchCart(this.state.activeCart)
			}
		)
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
						if (!localStorage.getItem('cart')) {
							this.saveToLocalStorage()
						}
						if (data.cartCompleted === 1) {
							const currentCartId = this.getCurrentCartFromLocalStorage()
							localStorage.removeItem('cart')

							localStorage.setItem(
								'currentCartId',
								JSON.stringify(currentCartId + 1)
							)
							return
						}
						this.state.cart.products.length > 0 && this.updateTotal()
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

	getCurrentCartFromLocalStorage = () => {
		const currentCart = JSON.parse(localStorage.getItem('cart'))
		return currentCart.cartId
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
