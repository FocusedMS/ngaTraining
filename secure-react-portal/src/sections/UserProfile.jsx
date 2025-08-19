import { useEffect, useState } from 'react'
import ProfileDetails from './ProfileDetails.jsx'

export default function UserProfile({ user, onLogout }) {
	const [status, setStatus] = useState('Logged Out')
	const [profile, setProfile] = useState(user)

	useEffect(() => {
		// Simulate fetch user data
		const timeout = setTimeout(() => {
			const stored = sessionStorage.getItem('sessionUser')
			const parsed = stored ? JSON.parse(stored) : user
			setProfile(parsed)
			setStatus(parsed ? 'Logged In' : 'Logged Out')
		}, 500)
		return () => clearTimeout(timeout)
	}, [user])

	return (
		<div style={{ border: '1px solid #ddd', padding: 16, borderRadius: 8 }}>
			<h3>User Profile</h3>
			<p><strong>Name:</strong> {profile?.name || 'Guest'}</p>
			<p><strong>Role:</strong> {profile?.role || 'user'}</p>
			<p><strong>Status:</strong> {status}</p>
			<ProfileDetails info={`Welcome ${profile?.name || 'Guest'}`} />
			<button onClick={() => { setStatus('Logged Out'); onLogout && onLogout(); }}>Logout</button>
		</div>
	)
}


