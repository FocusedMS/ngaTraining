import { API_URL } from './api'

const authHeader = (): HeadersInit => {
  const t = localStorage.getItem("token");
  return t ? { Authorization: `Bearer ${t}` } : {};
};

export async function getResume(id:number){
  const res = await fetch(`${API_URL}/api/resumes/${id}`, { headers: { ...authHeader() } })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}

export async function createResume(payload: any){
  const res = await fetch(`${API_URL}/api/resumes`, {
    method:'POST',
    headers: { 'Content-Type':'application/json', ...authHeader() },
    body: JSON.stringify(payload)
  })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}

export async function updateResume(id:number, payload:any){
  const res = await fetch(`${API_URL}/api/resumes/${id}`, {
    method:'PUT',
    headers: { 'Content-Type':'application/json', ...authHeader() },
    body: JSON.stringify(payload)
  })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}

export async function downloadPdf(id:number, template?: string){
  const q = template ? `?template=${encodeURIComponent(template)}` : ''
  const res = await fetch(`${API_URL}/api/resumes/download/${id}${q}`, { headers: { ...authHeader() } })
  if(!res.ok) throw new Error(await res.text())
  return res.blob()
}

export async function aiSuggest(id:number){
  const res = await fetch(`${API_URL}/api/resumes/${id}/ai-suggestions`, { method:'POST', headers: { ...authHeader() } })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}

export async function listResumes(all?: boolean){
  const q = all ? '?all=1' : ''
  const res = await fetch(`${API_URL}/api/resumes${q}`, { headers: { ...authHeader() } })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}

export async function aiSuggestPreview(id:number){
  const res = await fetch(`${API_URL}/api/resumes/${id}/ai-suggestions/preview`, { method:'POST', headers: { ...authHeader() } })
  if(!res.ok) throw new Error(await res.text())
  return res.json()
}

export async function deleteResume(id:number){
  const res = await fetch(`${API_URL}/api/resumes/${id}`, { method:'DELETE', headers: { ...authHeader() } })
  if(!res.ok) throw new Error(await res.text())
  return true
}