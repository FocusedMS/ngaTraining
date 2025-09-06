import { Link } from 'react-router-dom'

export default function TopNav(){
  const role = localStorage.getItem('role')
  const token = localStorage.getItem('token')
  return (
    <div className="nav">
      <div className="logo">
        <div className="logo-chip">AI</div>
        AI Resume Builder
      </div>
      <div className="nav-actions">
        <Link to="/">Home</Link>
        {token ? <Link to="/dashboard">Dashboard</Link> : null}
        {role === 'Admin' ? <Link to="/admin">Admin</Link> : null}
        {!token ? <Link to="/login">Login</Link> : <a href="#" onClick={()=>{localStorage.clear(); location.href='/'}}>Logout</a>}
      </div>
    </div>
  )
}