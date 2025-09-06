import { useEffect, useState } from 'react'
import { http } from '../services/api'

type UserItem = { id:string; email:string; fullName?:string }

export default function Admin(){
  const [users,setUsers] = useState<UserItem[]>([])
  const [metrics,setMetrics] = useState<any>(null)

  useEffect(()=>{
    (async ()=>{
      setUsers(await http<UserItem[]>('/api/admin/users'))
      setMetrics(await http('/api/admin/metrics'))
    })()
  },[])

  return (
    <div className="container">
      <div className="grid">
        <div className="card">
          <h3 style={{marginTop:0}}>Users</h3>
          <ul>
            {users.map(u => <li key={u.id}>{u.fullName ?? '(no name)'} — {u.email}</li>)}
          </ul>
        </div>
        <div className="card">
          <h3 style={{marginTop:0}}>Metrics</h3>
          {metrics && <ul>
            <li>Total users: {metrics.totalUsers}</li>
            <li>Total resumes: {metrics.totalResumes}</li>
            <li>Last 24h resumes: {metrics.last24hResumes}</li>
            <li>Last 7 days resumes: {metrics.last7dResumes}</li>
          </ul>}
        </div>
      </div>
    </div>
  )
}