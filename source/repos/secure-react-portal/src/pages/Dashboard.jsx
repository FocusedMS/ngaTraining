import { useEffect, useState } from 'react'
import UserProfile from '../sections/UserProfile.jsx'
import { Link } from 'react-router-dom'

export default function Dashboard({ initialUser, onLogout }) {
	const [user, setUser] = useState(initialUser || null)
	const [fetched, setFetched] = useState(false)

	// Prop drilling example: pass user and onLogout down to nested component
	useEffect(() => {
		if (!user) {
			// simulate fetching
			const timeout = setTimeout(() => {
				const stored = sessionStorage.getItem('sessionUser')
				const parsed = stored ? JSON.parse(stored) : { name: 'Guest', role: 'user' }
				setUser(parsed)
				setFetched(true)
			}, 600)
			return () => clearTimeout(timeout)
		} else {
			setFetched(true)
		}
	}, [user])

	if (!fetched) {
		return <p style={{ padding: 16 }}>Loading profile...</p>
	}

	return (
		<div style={{ maxWidth: 640, margin: '24px auto', padding: 16 }}>
			<h2>Dashboard</h2>
			<nav style={{ display: 'flex', gap: 12, marginBottom: 12 }}>
				<Link to="/profile">Profile</Link>
				<Link to="/admin">Admin</Link>
				<button onClick={onLogout}>Logout</button>
			</nav>
			<UserProfile user={user} onLogout={onLogout} />
		</div>
	)
}


