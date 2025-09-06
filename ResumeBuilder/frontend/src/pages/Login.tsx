import { useState } from 'react'
import { apiLogin } from '../services/auth'
import { useNavigate } from 'react-router-dom'

export default function Login(){
  const nav = useNavigate()
  const [email,setEmail] = useState('')
  const [password,setPassword] = useState('')
  const [toast,setToast] = useState('')

  const submit = async (e:React.FormEvent)=>{
    e.preventDefault()
    if(!email || !/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email)){
      setToast('Enter a valid email')
      setTimeout(()=>setToast(''), 2000)
      return
    }
    if(!password || password.length < 6){
      setToast('Password must be at least 6 characters')
      setTimeout(()=>setToast(''), 2000)
      return
    }
    try{
      const res = await apiLogin(email,password)
      localStorage.setItem('token', res.token)
      localStorage.setItem('role', res.role)
      localStorage.setItem('email', res.email)
      localStorage.setItem('name', res.name ?? '')
      nav('/dashboard')
    }catch(err:any){
      setToast(err?.message ?? 'Login failed')
      setTimeout(()=>setToast(''), 2500)
    }
  }

  return (
    <div className="container">
      <div className="card" style={{maxWidth:480, margin:'0 auto'}}>
        <h2 style={{marginTop:0}}>Login</h2>
        <form className="grid" onSubmit={submit}>
          <input className="input" placeholder="Email" value={email} onChange={e=>setEmail(e.target.value)} />
          <input className="input" placeholder="Password" type="password" value={password} onChange={e=>setPassword(e.target.value)} />
          <button className="btn" type="submit">Login</button>
          <div>New here? <a href="/register">Create an account</a></div>
        </form>
      </div>
      {toast && <div className="toast">{toast}</div>}
    </div>
  )
}