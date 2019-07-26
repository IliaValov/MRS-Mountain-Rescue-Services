import React, { Component } from 'react';
import { Map, GoogleApiWrapper } from 'google-maps-react';


export class GoogleMap extends Component {
  render() {
    return (
        <Map
        google={this.props.google}
        zoom={14}
        initialCenter={{
         lat: -1.2884,
         lng: 36.8233
        }}
      />
      
    );
  }
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyBWBwtK08PpLeiu2wVEyrr8P7x2LIqSrSk'
})(GoogleMap);