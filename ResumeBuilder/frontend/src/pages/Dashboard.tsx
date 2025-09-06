import { useEffect, useState } from 'react'
import { createResume, listResumes, deleteResume, downloadPdf } from '../services/resumes'
import { useNavigate } from 'react-router-dom'

export default function Dashboard(){
  const nav = useNavigate()
  const [id,setId] = useState<number|null>(null)
  const [title,setTitle] = useState('My Resume')
  const [toast,setToast] = useState('')
  const [items,setItems] = useState<any[]>([])
  const [showAll,setShowAll] = useState(false)

  const create = async ()=>{
    try{
      const newId = await createResume({ title, personalInfo:'', education:'', experience:'', skills:'' })
      setId(newId)
      nav(`/builder/${newId}`)
    }catch(e:any){
      setToast(e?.message ?? 'Failed')
      setTimeout(()=>setToast(''), 2500)
    }
  }

  const refresh = async (all?:boolean)=>{
    const list = await listResumes(all)
    setItems(list)
  }

  useEffect(()=>{
    (async ()=>{
      try{
        await refresh(showAll && localStorage.getItem('role') === 'Admin')
      }catch(e:any){ /* ignore */ }
    })()
  },[showAll])

  return (
    <div className="container">
      <div className="card">
        <h2 style={{marginTop:0}}>My Resumes</h2>
        <div className="grid grid-2">
          <div>
            <div className="section-title">New resume</div>
            <input className="input" value={title} onChange={e=>setTitle(e.target.value)} placeholder="Title" />
            <div style={{marginTop:12}}>
              <button className="btn" onClick={create}>Create & open builder</button>
            </div>
          </div>
          <div>
            <div className="section-title">Tip</div>
            <p style={{color:'var(--text-600)'}}>Start with a short summary and 3–6 quantified bullets in experience.</p>
          </div>
        </div>
        <div style={{marginTop:16}}>
          <table className="table">
            <thead>
              <tr>
                <th>Title</th>
                <th>Updated</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {items.map(it => (
                <tr key={it.resumeId}>
                  <td>{it.title}</td>
                  <td>{new Date(it.updatedAt).toLocaleString()}</td>
                  <td>
                    <button className="btn" onClick={()=>nav(`/builder/${it.resumeId}`)}>Edit</button>
                    <button className="btn" onClick={async()=>{
                      const blob = await downloadPdf(it.resumeId)
                      const url = URL.createObjectURL(blob)
                      const a = document.createElement('a')
                      a.href = url; a.download = `resume-${it.resumeId}.pdf`; a.click()
                      URL.revokeObjectURL(url)
                    }}>Download</button>
                    <button className="btn" onClick={async()=>{
                      const ok = confirm('Delete this resume? This cannot be undone.')
                      if(!ok) return
                      try{
                        await deleteResume(it.resumeId)
                        await refresh(showAll && localStorage.getItem('role') === 'Admin')
                        setToast('Deleted')
                        setTimeout(()=>setToast(''), 1200)
                      }catch(e:any){ setToast('Delete failed'); setTimeout(()=>setToast(''), 2000) }
                    }}>Delete</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          {localStorage.getItem('role') === 'Admin' && (
            <div style={{marginTop:12}}>
              <label style={{display:'inline-flex', gap:8, alignItems:'center'}}>
                <input type="checkbox" checked={showAll} onChange={e=>setShowAll(e.target.checked)} />
                Show all resumes (Admin)
              </label>
            </div>
          )}
        </div>
      </div>
      {toast && <div className="toast">{toast}</div>}
    </div>
  )
}