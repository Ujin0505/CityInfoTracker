import React, { Component, useState, useEffect } from 'react';
import {Button, Label, Input, Spinner, Container} from 'reactstrap'
import {getCityInfo} from '../api';

export const Home = () => {
  const [zipCode, setZipCode] = useState(0);
  const [cityInfo, setCityInfo] = useState(null);

  const [isLoaded, setLoaded] = useState(false);
  const [isError, setError] = useState(false);

  const handleInputChange = (e) =>{
    setZipCode(e.target.value);
  }

  const handleClick = (e) =>{
    setLoaded(true)
  }
  
  useEffect(() =>{
    const fetchData = async() =>{
      try {
        const data = await getCityInfo(zipCode);
        setError(false);
        setCityInfo({...data});
      }
      catch (e) {
        setCityInfo(null);
        setError(true);
      }
      finally {
        setLoaded(false);
      }
    }
    
    if(isLoaded)
      fetchData()
    
  }, [isLoaded])
  
  let info;
  if(cityInfo)
  {
    info =  cityInfo && <div>
      <p>{cityInfo.name}</p>
      <p>{cityInfo.temperature}</p>
      <p>{cityInfo.timeZone}</p>
    </div>
  }
  else
  {
    if(isError)
      info = <div> Error zip code</div>
  }
  
    return (
      <div>
        <div>
          <Input type="text"  placeholder="Input zip code...(f.e. 10028)" onChange={(e) => handleInputChange(e)}/>
          <Button disabled={isLoaded} onClick={(e) => handleClick(e)}>Get</Button>
        </div>
        <div>
          {
            isLoaded ? <Spinner/> : info
          }
        </div>
      </div>
    );

}
