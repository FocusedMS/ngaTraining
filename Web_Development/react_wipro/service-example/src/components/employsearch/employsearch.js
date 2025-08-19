import axios from 'axios';
import React, {Component, useState} from 'react';

const EmploySearch = () => {
  const [empno, SetEmpno] = useState(0)
  const [employData, SetEmployData] = useState({});
  const [errorMessage, setErrorMessage] = useState(null);

 const handleChange = event => {
        SetEmpno(event.target.value)
    }

    const show = async () => {
      try {
        let eid = parseInt(empno);
        const response = await axios.get("https://localhost:7209/api/Employs/"+eid)
        SetEmployData(response.data)
        setErrorMessage(null)
      } catch (err) {
        setErrorMessage((err && err.message) ? err.message : 'Network request failed')
      }
    }

    return(
      <div>
         <label>
                Employ No : </label>
            <input type="number" name="empno" 
                value={empno} onChange={handleChange} /> <br/>
            <input type="button" value="Show" onClick={show} />
            <hr/>
            {errorMessage && <div style={{color:'red'}}>Error: {errorMessage}</div>}
            Employ No : <b> {employData.empno}</b> <br/>
            Employ Name : <b>{employData.name}</b> <br/>
            Gender : <b>{employData.gender}</b> <br/>
            Department : <b>{employData.dept}</b> <br/>
            Designation : <b>{employData.desig}</b> <br/>
            Salary : <b>{employData.basic}</b>
      </div>
    )
  

}

export default EmploySearch;