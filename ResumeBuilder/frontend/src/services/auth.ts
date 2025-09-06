import { API_URL } from './api'

export async function apiLogin(email:string, password:string){
  const res = await fetch(`${API_URL}/api/auth/login`, {
    method: 'POST',
    headers: { 'Content-Type':'application/json' },
    body: JSON.stringify({ email, password })
  })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}

export async function apiRegister(email:string, password:string, fullName?:string){
  const res = await fetch(`${API_URL}/api/auth/register`, {
    method: 'POST',
    headers: { 'Content-Type':'application/json' },
    body: JSON.stringify({ email, password, fullName })
  })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}