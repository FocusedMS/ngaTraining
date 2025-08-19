import { Link } from 'react-router-dom'

export default function AdminDashboard({ user, onLogout }) {
	return (
		<div style={{ maxWidth: 720, margin: '24px auto', padding: 16 }}>
			<h2>Admin Dashboard</h2>
			<nav style={{ display: 'flex', gap: 12, marginBottom: 12 }}>
				<Link to="/profile">Profile</Link>
				<button onClick={onLogout}>Logout</button>
			</nav>
			<p>Welcome, {user?.name || 'Admin'}.</p>
			<div style={{ border: '1px solid #ddd', padding: 16, borderRadius: 8 }}>
				<h3>User Management</h3>
				<ul>
					<li>View users</li>
					<li>Create user</li>
					<li>Delete user</li>
				</ul>
			</div>
		</div>
	)
}


