import React from 'react'
import PropTypes from 'prop-types'
import { withStyles } from '@material-ui/core/styles'
import Card from '@material-ui/core/Card'
import CardActionArea from '@material-ui/core/CardActionArea'
import CardActions from '@material-ui/core/CardActions'
import CardContent from '@material-ui/core/CardContent'
import CardMedia from '@material-ui/core/CardMedia'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'

const styles = {
	card: {
		maxWidth: 345
	},
	media: {
		objectFit: 'cover'
	}
}

const ProductCard = props => {
	const { classes } = props

	const handleClick = productId => {
		const api = `https://localhost:5001/api/product/${productId}/addtocart`
		const currentCart = JSON.parse(localStorage.getItem('cart'))
		const currentCartId = JSON.parse(localStorage.getItem('currentCartId'))
		console.log(currentCart, currentCartId)

		const data = {
			CartId: currentCart !== null ? currentCart.cartId : currentCartId,
			ProductId: productId
		}

		fetch(api, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(data)
		})
			.then(res => res.json())
			.then(json => console.log(json))
	}

	return (
		<Card className={classes.card}>
			<CardActionArea>
				<CardMedia
					component="img"
					alt={props.title}
					className={classes.media}
					height="140"
					src={props.image}
					title={props.title}
				/>
				<CardContent>
					<Typography gutterBottom variant="h5" component="h2">
						{props.title}
					</Typography>
					<Typography component="p">{props.description}</Typography>
				</CardContent>
			</CardActionArea>
			<CardActions>
				<Button
					size="small"
					color="primary"
					onClick={() => handleClick(props.id)}
				>
					Add to cart
				</Button>
				<p>{props.price} SEK</p>
			</CardActions>
		</Card>
	)
}

ProductCard.propTypes = {
	classes: PropTypes.object.isRequired
}

export default withStyles(styles)(ProductCard)
