import axios from 'axios';
import React, {Component, useEffect, useState} from 'react';

const EmployShow = () => {

  const [employs,setEmployData] = useState([])
  const [errorMessage, setErrorMessage] = useState(null)

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("https://localhost:7209/api/Employs")
        setEmployData(response.data)
        setErrorMessage(null)
      } catch (err) {
        setErrorMessage((err && err.message) ? err.message : 'Network request failed')
      }
    }
    fetchData();
  },[])
  return(
    <div>
      {errorMessage && <div style={{color:'red'}}>Error: {errorMessage}</div>}
      <table border="3" align="center">
        <tr>
          <th>Employ No</th>
          <th>Employ Name</th>
          <th>Gender</th>
          <th>Department</th>
          <th>Designation</th>
          <th>Basic</th>
        </tr>
        {employs.map((item) =>
        <tr>
          <td>{item.empno}</td>
           <td>{item.name}</td>
            <td>{item.gender}</td>
           <td>{item.dept}</td>
           <td>{item.desig}</td>
           <td>{item.basic}</td>
        </tr>
      )}
      </table>
    </div>
  )
}

export default EmployShow;