import React from 'react'
import PropTypes from 'prop-types'
import { withStyles } from '@material-ui/core/styles'
import Typography from '@material-ui/core/Typography'
import Modal from '@material-ui/core/Modal'
import Button from '@material-ui/core/Button'

const styles = theme => ({
	paper: {
		position: 'absolute',
		width: theme.spacing.unit * 50,
		backgroundColor: theme.palette.background.paper,
		boxShadow: theme.shadows[5],
		padding: theme.spacing.unit * 4,
		outline: 'none'
	}
})

class SimpleModal extends React.Component {
	render() {
		const { classes } = this.props

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
							onClick={this.props.toggleModal}
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
