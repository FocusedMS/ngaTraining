import { Component } from 'react'
import DOMPurify from 'dompurify'
import { nanoid } from 'nanoid'

export default class Login extends Component {
	constructor(props) {
		super(props)
		this.state = {
			username: '',
			password: '',
			error: '',
			csrfToken: ''
		}
	}

	componentDidMount() {
		const token = nanoid(24)
		this.setState({ csrfToken: token })
		sessionStorage.setItem('csrfToken', token)
	}

	componentDidUpdate(prevProps, prevState) {
		if (this.props.isAuthenticated && !prevProps.isAuthenticated) {
			// Successful login observed
		}
		if (prevState.error !== this.state.error && this.state.error) {
			// Error state changed
		}
	}

	componentWillUnmount() {
		// Clear transient login state on unmount
		this.setState({ username: '', password: '', error: '' })
	}

	handleChange = (e) => {
		const { name, value } = e.target
		const clean = DOMPurify.sanitize(value)
		this.setState({ [name]: clean })
	}

	handleSubmit = (e) => {
		e.preventDefault()
		const { username, password, csrfToken } = this.state
		if (!username || !password) {
			this.setState({ error: 'Username and password are required.' })
			return
		}
		const stored = sessionStorage.getItem('csrfToken')
		if (!stored || stored !== csrfToken) {
			this.setState({ error: 'Invalid CSRF token. Please refresh the page.' })
			return
		}
		const sanitizedUsername = DOMPurify.sanitize(username)
		const role = sanitizedUsername.toLowerCase() === 'admin' ? 'admin' : 'user'
		this.setState({ error: '' })
		this.props.onLogin({ username: sanitizedUsername, role })
	}

	render() {
		const { isAuthenticated } = this.props
		const { username, password, error } = this.state
		return (
			<div style={{ maxWidth: 360, margin: '40px auto' }}>
				<h2>Login</h2>
				{isAuthenticated ? <p>You are already logged in.</p> : null}
				<form onSubmit={this.handleSubmit} noValidate>
					<div style={{ marginBottom: 12 }}>
						<label htmlFor="username">Username</label>
						<input id="username" name="username" type="text" value={username} onChange={this.handleChange} required style={{ width: '100%' }} />
					</div>
					<div style={{ marginBottom: 12 }}>
						<label htmlFor="password">Password</label>
						<input id="password" name="password" type="password" value={password} onChange={this.handleChange} required style={{ width: '100%' }} />
					</div>
					<input type="hidden" name="csrfToken" value={this.state.csrfToken} readOnly />
					<button type="submit">Login</button>
				</form>
				{error ? <p style={{ color: 'red' }}>{error}</p> : null}
			</div>
		)
	}
}


