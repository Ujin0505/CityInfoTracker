import React, { Component, useState, useEffect } from 'react';
import {Button, Label, Input, Spinner, Container, Row, Col,Card, CardBody} from 'reactstrap'
import {getCityInfo} from '../api';
import {MapWithAMarker} from "./Map";
import {CityInfo} from "./CityInfo";


const API_KEY = process.env["REACT_APP_GOOGLE_API_KEY"]
const URL = `https://maps.googleapis.com/maps/api/js?key=${API_KEY}&language=en&v=3.exp&libraries=geometry,drawing,places`
console.log(URL);

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
        console.log(data);
        setError(false);
        setCityInfo( {...data}, parseFloat(data.latitude), parseFloat(data.longtitude));
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
    info =  <CityInfo name={cityInfo.name} temp={cityInfo.temperature} timeZone={cityInfo.timeZone}/>
  else
  {
    info = isError ?  <div> Error zip code</div> : <CityInfo name="City" temp="0" timeZone="TimeZone"/>
  }
  
    return (
      <Container>
        <Row>
          <Col xs={11}>
            <Input type="text"  placeholder="Input zip code...(f.e. 10028)" onChange={(e) => handleInputChange(e)}/>
          </Col>
          <Col xs={1}>
            <Button disabled={isLoaded} onClick={(e) => handleClick(e)}>Get</Button>
          </Col>
        </Row>
        <Row>
          <Col xs={7}>
            <MapWithAMarker
                
                markerPos={{lat: cityInfo ? cityInfo.latitude : 0 , lng:cityInfo ? cityInfo.longtitude: 0 }}
                googleMapURL={URL}
                loadingElement={<div style={{ height: `100%` }} />}
                containerElement={<div style={{ height: `400px` }} />}
                mapElement={<div style={{ height: `100%` }} />}
            />
          </Col >
          <Col xs={4}> 
            {
              isLoaded ? <Spinner/> : info
            }
          </Col>
        </Row>
      </Container>
    );

}
