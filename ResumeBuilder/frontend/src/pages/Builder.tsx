import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { getResume, updateResume, downloadPdf, aiSuggest, aiSuggestPreview } from '../services/resumes'

export default function Builder(){
  const { id } = useParams()
  const [title,setTitle] = useState('')
  const [personalInfo,setPersonalInfo] = useState('')
  const [education,setEducation] = useState('')
  const [experience,setExperience] = useState('')
  const [skills,setSkills] = useState('')
  const [sugs,setSugs] = useState<any[]>([])
  const [template,setTemplate] = useState('classic')
  const [toast,setToast] = useState('')

  useEffect(()=>{
    (async ()=>{
      try{
        const r = await getResume(Number(id))
        setTitle(r.title ?? '')
        setPersonalInfo(r.personalInfo ?? '')
        setEducation(r.education ?? '')
        setExperience(r.experience ?? '')
        setSkills(r.skills ?? '')
      }catch(e:any){
        setToast('Failed to load')
        setTimeout(()=>setToast(''), 2500)
      }
    })()
  },[id])

  const save = async ()=>{
    try{
      await updateResume(Number(id), { title, personalInfo, education, experience, skills })
      setToast('Saved')
      setTimeout(()=>setToast(''), 1000)
    }catch(e:any){
      setToast('Save failed')
      setTimeout(()=>setToast(''), 2500)
    }
  }

  const dl = async ()=>{
    try{
      const blob = await downloadPdf(Number(id), template)
      const url = URL.createObjectURL(blob)
      const a = document.createElement('a')
      a.href = url; a.download = `resume-${id}.pdf`; a.click()
      URL.revokeObjectURL(url)
    }catch(e:any){
      setToast('Download failed')
      setTimeout(()=>setToast(''), 2500)
    }
  }

  const suggest = async ()=>{
    try{
      const res = await aiSuggest(Number(id))
      setSugs(res.suggestions ?? [])
    }catch(e:any){
      setToast('AI failed')
      setTimeout(()=>setToast(''), 2500)
    }
  }

  const previewSuggest = async ()=>{
    try{
      const res = await aiSuggestPreview(Number(id))
      setSugs(res.suggestions ?? [])
    }catch(e:any){
      setToast('AI preview failed')
      setTimeout(()=>setToast(''), 2500)
    }
  }

  const applySuggestion = (s:any)=>{
    if(s.section === 'PersonalInfo') setPersonalInfo(prev => prev ? prev + '\n' + (s.applySnippet||'') : (s.applySnippet||''))
    if(s.section === 'Education') setEducation(prev => prev ? prev + '\n' + (s.applySnippet||'') : (s.applySnippet||''))
    if(s.section === 'Experience') setExperience(prev => prev ? prev + '\n' + (s.applySnippet||'') : (s.applySnippet||''))
    if(s.section === 'Skills') setSkills(prev => prev ? prev + '\n' + (s.applySnippet||'') : (s.applySnippet||''))
  }

  return (
    <div className="container">
      <div className="grid grid-2">
        <div className="card">
          <h3 style={{marginTop:0}}>Editor</h3>
          <div className="grid">
            <input className="input" value={title} onChange={e=>setTitle(e.target.value)} placeholder="Title" />
            <textarea className="input" rows={4} value={personalInfo} onChange={e=>setPersonalInfo(e.target.value)} placeholder="Personal summary" />
            <textarea className="input" rows={5} value={education} onChange={e=>setEducation(e.target.value)} placeholder="Education" />
            <textarea className="input" rows={6} value={experience} onChange={e=>setExperience(e.target.value)} placeholder="Experience (use bullets)" />
            <textarea className="input" rows={3} value={skills} onChange={e=>setSkills(e.target.value)} placeholder="Skills (comma separated)" />
            <div style={{display:'flex', gap:8}}>
              <button className="btn" onClick={save}>Save</button>
              <select className="input" value={template} onChange={e=>setTemplate(e.target.value)}>
                <option value="classic">Classic PDF</option>
                <option value="compact">Compact PDF</option>
              </select>
              <button className="btn" onClick={dl}>Download PDF</button>
              <button className="btn" onClick={suggest}>Save + AI suggestions</button>
              <button className="btn" onClick={previewSuggest}>Preview AI suggestions</button>
            </div>
          </div>
          <div style={{marginTop:16}}>
            {!!sugs.length && <>
              <div className="section-title">Suggestions</div>
              <ul>
                {sugs.map((s:any, i:number)=>(
                  <li key={i}>
                    <div>{s.message}</div>
                    {s.applySnippet && <div style={{marginTop:6}}>
                      <button className="btn" onClick={()=>applySuggestion(s)}>Apply</button>
                    </div>}
                  </li>
                ))}
              </ul>
            </>}
          </div>
        </div>
        <div className="card">
          <h3 style={{marginTop:0}}>Live Preview</h3>
          <div className="preview">
            <h2 style={{marginTop:0}}>{title}</h2>
            {personalInfo && <><div className="section-title">Personal</div><div className="separator" /><p>{personalInfo}</p></>}
            {education && <><div className="section-title">Education</div><div className="separator" /><p style={{whiteSpace:'pre-wrap'}}>{education}</p></>}
            {experience && <><div className="section-title">Experience</div><div className="separator" /><p style={{whiteSpace:'pre-wrap'}}>{experience}</p></>}
            {skills && <><div className="section-title">Skills</div><div className="separator" /><p>{skills}</p></>}
          </div>
        </div>
      </div>
      {toast && <div className="toast">{toast}</div>}
    </div>
  )
}