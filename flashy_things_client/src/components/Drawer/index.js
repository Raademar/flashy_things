import React from 'react'
import PropTypes from 'prop-types'
import { withStyles } from '@material-ui/core/styles'
import Drawer from '@material-ui/core/Drawer'
import Button from '@material-ui/core/Button'
import List from '@material-ui/core/List'
import ListItem from '@material-ui/core/ListItem'
import ShoppingCart from '@material-ui/icons/ShoppingCart'

const styles = {
	list: {
		width: 450
	},
	fullList: {
		width: 'auto'
	},
	span: {
		paddingRight: 5
	},
	rightIcon: {
		marginLeft: 5
	}
}

class ShoppingCartDrawer extends React.Component {
	state = {
		right: false
	}

	render() {
		const { classes } = this.props
		const sideList = (
			<div className={classes.list}>
				<List>
					{this.props.cart.products.map((item, index) => (
						<ListItem key={index}>
							<span className={classes.span}>1 x </span>
							<img src={item.image} alt="" width="40px" height="auto" />
							<p>{item.title}</p>
						</ListItem>
					))}
					<p>Total price: {this.props.totalPrice} SEK</p>
					<Button variant="contained" color="primary">
						Go to checkout
						<ShoppingCart className={classes.rightIcon} />
					</Button>
				</List>
			</div>
		)

		return (
			<div>
				<Drawer
					anchor="right"
					open={this.props.drawerOpen}
					onClose={this.props.toggleDrawer}
				>
					<div
						tabIndex={0}
						role="button"
						onClick={this.props.toggleDrawer}
						onKeyDown={this.props.toggleDrawer}
					>
						{sideList}
					</div>
				</Drawer>
			</div>
		)
	}
}

ShoppingCartDrawer.propTypes = {
	classes: PropTypes.object.isRequired
}

export default withStyles(styles)(ShoppingCartDrawer)
