import React, {Component, useState} from 'react';

const Four = () => {

  const [name,setName] = useState('')

  const ajay = () => {
    setName("Hi I am Ajay");
  }

  const pravali = () => {
    setName("Hi I am Pravali...");
  }

  const uday = () => {
    setName("Hi I am Uday...");
  }

  return(
    <div>
      <input type="button" value="Ajay" onClick={ajay} />
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <input type="button" value="Pravali" onClick={pravali} /> 
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <input type="button" value="Uday" onClick={uday} />
      <hr/>
      Name is : <b>{name}</b>
    </div>
  )
}

export default Four;
