import { Routes, Route, Navigate } from 'react-router-dom'
import Home from './pages/Home'
import Login from './pages/Login'
import Register from './pages/Register'
import Dashboard from './pages/Dashboard'
import Builder from './pages/Builder'
import Admin from './pages/Admin'
import TopNav from './components/layout/TopNav'

export default function App(){
  return (
    <div className="app">
      <TopNav />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/dashboard" element={<RequireAuth><Dashboard /></RequireAuth>} />
        <Route path="/builder/:id" element={<RequireAuth><Builder /></RequireAuth>} />
        <Route path="/admin" element={<RequireAdmin><Admin /></RequireAdmin>} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </div>
  )
}

function RequireAuth({ children }:{ children: JSX.Element }){
  const token = localStorage.getItem('token')
  return token ? children : <Navigate to="/login" replace />
}
function RequireAdmin({ children }:{ children: JSX.Element }){
  const token = localStorage.getItem('token')
  const role = localStorage.getItem('role')
  return token && role === 'Admin' ? children : <Navigate to="/" replace />
}