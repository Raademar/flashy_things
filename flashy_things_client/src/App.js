import React, { Component } from 'react'
import './App.css'
import Header from './components/Header'
import ProductGrid from './components/ProductGrid'
import ShoppingCartDrawer from './components/Drawer'

class App extends Component {
  state = {
    products: []
  }

  componentDidMount() {
    this.fetchProducts()
  }
  
  fetchProducts = () => {
    const api = 'https://localhost:5001/api/product'
    fetch(api)
      .then(res => res.json())
      .then(data => this.setState({
        products: data.value
      }))
  }

  render() {
    return (
      <div className="App">
        <Header />
        <ShoppingCartDrawer />
        <ProductGrid products={this.state.products} />
      </div>
    )
  }
}

export default App
