export default function Home(){
  return (
    <div className="container">
      <div className="card">
        <h1 style={{marginTop:0}}>Build a better resume, faster.</h1>
        <p style={{color:'var(--text-600)'}}>Create, edit and download polished resumes. Get AI suggestions to level up your content. Secure, role‑based access.</p>
        <div style={{marginTop:16}}>
          <a className="btn" href="/register">Get Started</a>
        </div>
      </div>
    </div>
  )
}