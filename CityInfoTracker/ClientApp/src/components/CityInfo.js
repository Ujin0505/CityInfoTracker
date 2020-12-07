import React from "react";
import {Card, CardBody} from "reactstrap"

export const CityInfo = ({name, temp, timeZone}) =>{
    return(
        <Card>
            <CardBody>
                <p>City: {name}</p>
                <p>Temp: {temp}</p>
                <p>TimeZone: {timeZone}</p>
            </CardBody>
        </Card>
    )
    
}
