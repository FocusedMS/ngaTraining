import { Navigate } from 'react-router-dom'

export default function ProtectedRoute({ isAuthenticated, role, requiredRole, children }) {
	if (!isAuthenticated) {
		return <Navigate to="/login" replace />
	}
	if (requiredRole && role !== requiredRole) {
		return <Navigate to="/login" replace />
	}
	return children
}


