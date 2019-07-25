import React from 'react';
import { connect } from 'react-redux';
import GoogleMap from './GoogleMap'
import "../../../assets/styles/Map.css";



export class MapContainer extends React.Component {

    render() {
        return (
            <div>
                <div className="nav-bar-vertical">
                        <p unselectable="on">item1</p>
                </div>

                <div className="map-container">
                    <GoogleMap />
                </div> 
            </div>
        )
    }
}

export default connect()(MapContainer);