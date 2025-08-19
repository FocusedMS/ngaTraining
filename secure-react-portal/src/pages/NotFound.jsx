import { Link } from 'react-router-dom'

export default function NotFound() {
	return (
		<div style={{ textAlign: 'center', marginTop: 80 }}>
			<h2>404 - Not Found</h2>
			<p>The page you are looking for does not exist.</p>
			<p>
				<Link to="/login">Go to Login</Link>
			</p>
		</div>
	)
}


