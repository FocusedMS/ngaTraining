import { useState } from 'react'
import { apiRegister } from '../services/auth'
import { useNavigate } from 'react-router-dom'

export default function Register(){
  const nav = useNavigate()
  const [email,setEmail] = useState('')
  const [password,setPassword] = useState('')
  const [name,setName] = useState('')
  const [toast,setToast] = useState('')

  const submit = async (e:React.FormEvent)=>{
    e.preventDefault()
    if(!name || name.trim().length < 2){
      setToast('Enter your full name')
      setTimeout(()=>setToast(''), 2000)
      return
    }
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
      await apiRegister(email,password,name)
      setToast('Registered! Please login.')
      setTimeout(()=>{ setToast(''); nav('/login') }, 1200)
    }catch(err:any){
      setToast(err?.message ?? 'Failed')
      setTimeout(()=>setToast(''), 2500)
    }
  }

  return (
    <div className="container">
      <div className="card" style={{maxWidth:520, margin:'0 auto'}}>
        <h2 style={{marginTop:0}}>Create account</h2>
        <form className="grid" onSubmit={submit}>
          <input className="input" placeholder="Full name" value={name} onChange={e=>setName(e.target.value)} />
          <input className="input" placeholder="Email" value={email} onChange={e=>setEmail(e.target.value)} />
          <input className="input" placeholder="Password" type="password" value={password} onChange={e=>setPassword(e.target.value)} />
          <button className="btn" type="submit">Register</button>
          <div>Have an account? <a href="/login">Login</a></div>
        </form>
      </div>
      {toast && <div className="toast">{toast}</div>}
    </div>
  )
}