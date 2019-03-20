import React from 'react'
import PropTypes from 'prop-types'
import { withStyles } from '@material-ui/core/styles'
import Grid from '@material-ui/core/Grid'
import ProductCard from '../ProductCard'

const styles = theme => ({
	root: {
		flexGrow: 1
	},
	paper: {
		padding: theme.spacing.unit * 2,
		textAlign: 'center',
		color: theme.palette.text.secondary
	},
	video: {
		width: '100%'
	}
})

function ProductGrid(props) {
	const { classes } = props

	return (
		<div className={classes.root}>
			<Grid container spacing={24} justify="center">
				<Grid item xs={12}>
					<video
						muted
						autoPlay
						loop
						src="https://nikevideo.nike.com/72451143001/201903/1110/72451143001_6011071974001_6011066272001.mp4"
						className={classes.video}
					/>
				</Grid>
				{props.products.map((item, i) => (
					<Grid item xs={6} sm={3}>
						<ProductCard
							image={item.image}
							title={item.title}
							description={item.description}
							key={i}
							id={item.id}
							price={item.price}
						/>
					</Grid>
				))}
			</Grid>
		</div>
	)
}

ProductGrid.propTypes = {
	classes: PropTypes.object.isRequired
}

export default withStyles(styles)(ProductGrid)
