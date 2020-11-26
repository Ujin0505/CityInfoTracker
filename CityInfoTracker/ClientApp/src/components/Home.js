import React, { Component, useState, useEffect } from 'react';
import {getCityInfo} from '../api';

export const Home = () => {
  const [zipCode, setZipCode] = useState(0);
  const [cityInfo, setCityInfo] = useState(/*{name:'', temperature:0, timezone:''}*/);

  const handleInputChange = (e) =>{
    setZipCode(e.target.value);
  }

  const handleClick = async (e) =>{
    const data = await getCityInfo(zipCode);
    console.log(data);
    setCityInfo({...data});
  }
  
  const info = cityInfo && <div>
    <p>{cityInfo.name}</p>
    <p>{cityInfo.temperature}</p>
    <p>{cityInfo.timeZone}</p>  
  </div>

    return (
      <div>
        <input type="text" onChange={(e) => handleInputChange(e)}/>
        <button onClick={(e) => handleClick(e)}>Get</button>
        {info}
      </div>
    );

}
