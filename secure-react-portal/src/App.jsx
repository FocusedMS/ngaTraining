import { useEffect, useState } from 'react'
import { BrowserRouter, Routes, Route, Navigate, useNavigate } from 'react-router-dom'
import './App.css'
import Login from './pages/Login.jsx'
import Dashboard from './pages/Dashboard.jsx'
import AdminDashboard from './pages/AdminDashboard.jsx'
import NotFound from './pages/NotFound.jsx'
import ProtectedRoute from './components/ProtectedRoute.jsx'

function AppRouter() {
  const navigate = useNavigate()
  const [isAuthenticated, setIsAuthenticated] = useState(false)
  const [role, setRole] = useState('guest')
  const [user, setUser] = useState(null)

  useEffect(() => {
    const stored = sessionStorage.getItem('sessionUser')
    if (stored) {
      const parsed = JSON.parse(stored)
      setIsAuthenticated(true)
      setRole(parsed.role || 'user')
      setUser(parsed)
    }
  }, [])

  const handleLogin = (loginResult) => {
    setIsAuthenticated(true)
    setRole(loginResult.role)
    setUser({ name: loginResult.username, role: loginResult.role })
    sessionStorage.setItem('sessionUser', JSON.stringify({ name: loginResult.username, role: loginResult.role }))
    navigate('/profile')
  }

  const handleLogout = () => {
    sessionStorage.removeItem('sessionUser')
    sessionStorage.removeItem('csrfToken')
    setIsAuthenticated(false)
    setRole('guest')
    setUser(null)
    navigate('/login')
  }

  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" replace />} />
      <Route path="/login" element={<Login isAuthenticated={isAuthenticated} onLogin={handleLogin} />} />
      <Route path="/profile" element={<Dashboard initialUser={user} onLogout={handleLogout} />} />
      <Route
        path="/admin"
        element={
          <ProtectedRoute isAuthenticated={isAuthenticated} role={role} requiredRole="admin">
            <AdminDashboard onLogout={handleLogout} user={user} />
          </ProtectedRoute>
        }
      />
      <Route path="*" element={<NotFound />} />
    </Routes>
  )
}

export default function App() {
  return (
    <BrowserRouter>
      <AppRouter />
    </BrowserRouter>
  )
}
