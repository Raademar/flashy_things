import React from 'react'
import PropTypes from 'prop-types'
import { withStyles } from '@material-ui/core/styles'
import Typography from '@material-ui/core/Typography'
import Modal from '@material-ui/core/Modal'
import Button from '@material-ui/core/Button'
import TextField from '@material-ui/core/TextField'
import './modal.css'

const styles = theme => ({
	paper: {
		position: 'absolute',
		width: theme.spacing.unit * 50,
		backgroundColor: theme.palette.background.paper,
		boxShadow: theme.shadows[5],
		padding: theme.spacing.unit * 4,
		outline: 'none'
	},
	textField: {
		marginLeft: theme.spacing.unit,
		marginRight: theme.spacing.unit,
		width: 400
	}
})

class SimpleModal extends React.Component {
	constructor(props) {
		super(props)
		this.state = {
			customerinfo: {
				name: '',
				street: '',
				city: '',
				zipCode: '',
				telephone: '',
				email: '',
				currency: 'SEK'
			}
		}
	}

	handleChange = name => event => {
		this.setState({ [name]: event.target.value })
	}

	componentDidMount() {
		this.setState({
			cart: this.props.cart
		})
	}

	submitOrder = id => {
		const api = `https://localhost:5001/api/cart/${id}`
		const order = {
			cartId: id,
			name: this.state.name,
			street: this.state.street,
			city: this.state.city,
			zipCode: this.state.zipCode,
			telephone: this.state.name,
			email: this.state.email,
			price: this.props.cart.price
		}

		fetch(api, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(order)
		}).then(res => {
			res.status < 300 ? this.orderCompleted() : this.orderFailed()
		})
	}

	orderCompleted = () => {
		this.props.toggleModal()
	}

	orderFailed = () => {
		console.log('order failed')
	}

	render() {
		const { classes } = this.props
		const cart = JSON.parse(localStorage.getItem('cart')) || { products: [] }
		const newCartId = JSON.parse(localStorage.getItem('currentCartId'))

		return (
			<div>
				<Modal
					aria-labelledby="simple-modal-title"
					aria-describedby="simple-modal-description"
					open={this.props.modalOpen}
				>
					<div className={classes.paper}>
						<Typography variant="h6" id="modal-title">
							Confirm your order.
						</Typography>
						<Typography variant="subtitle1" id="simple-modal-description">
							Please fill in your delivery information.
						</Typography>
						<div className="cartOverview">
							{cart.products &&
								cart.products.map(item => (
									<div>
										<img src={item.image} alt="" width="40px" height="auto" />
										<p>{item.title}</p>
									</div>
								))}
							<span>{cart.price} SEK</span>
						</div>
						<div className="orderFields">
							<TextField
								id="name"
								variant="outlined"
								label="Name"
								className={classes.textField}
								value={this.state.name}
								onChange={this.handleChange('name')}
								margin="normal"
							/>
							<TextField
								id="street"
								variant="outlined"
								label="Street"
								className={classes.textField}
								value={this.state.street}
								onChange={this.handleChange('street')}
								margin="normal"
							/>
							<TextField
								id="city"
								variant="outlined"
								label="City"
								className={classes.textField}
								value={this.state.city}
								onChange={this.handleChange('city')}
								margin="normal"
							/>
							<TextField
								id="zipCode"
								variant="outlined"
								label="Zip Code"
								className={classes.textField}
								value={this.state.zipCode}
								onChange={this.handleChange('zipCode')}
								margin="normal"
							/>
							<TextField
								id="telephone"
								variant="outlined"
								label="Telephone"
								className={classes.textField}
								value={this.state.telephone}
								onChange={this.handleChange('telephone')}
								margin="normal"
							/>
							<TextField
								id="email"
								variant="outlined"
								label="Email"
								className={classes.textField}
								value={this.state.email}
								onChange={this.handleChange('email')}
								margin="normal"
							/>
						</div>
						<SimpleModalWrapped />
						<Button
							variant="contained"
							color="secondary"
							onClick={this.props.toggleModal}
						>
							Close checkout
						</Button>
						<Button
							variant="contained"
							color="primary"
							onClick={() => this.submitOrder(cart.cartId || newCartId)}
						>
							Submit order
						</Button>
					</div>
				</Modal>
			</div>
		)
	}
}

SimpleModal.propTypes = {
	classes: PropTypes.object.isRequired
}

// We need an intermediary variable for handling the recursive nesting.
const SimpleModalWrapped = withStyles(styles)(SimpleModal)

export default SimpleModalWrapped
