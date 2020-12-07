import React, {useState, useEffect} from "react";
import { withScriptjs, withGoogleMap, GoogleMap, Marker } from "react-google-maps";

export const MapWithAMarker = withScriptjs(withGoogleMap(props =>
{
    const [markerPos, setMarkerPos] = useState({lat:38.5,  lng:-98})
    
    useEffect(() => {
        setMarkerPos(props.markerPos)
        console.log(props.markerPos)
    }, [props.markerPos])
    
    return <GoogleMap
        defaultZoom={4}
        defaultCenter={{ lat: markerPos.lat, lng: markerPos.lng }}
    >
        <Marker
            position={{ lat: markerPos.lat ,  lng: markerPos.lng }}
        />
    </GoogleMap>
}
));
 

