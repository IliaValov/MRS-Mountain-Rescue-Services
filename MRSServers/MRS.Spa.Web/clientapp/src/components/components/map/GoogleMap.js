import React, { Component } from 'react';
import { Map, GoogleApiWrapper } from 'google-maps-react';


export class GoogleMap extends Component {
  render() {
    return (
      <div>
        <Map
        google={this.props.google}
        zoom={14}
        initialCenter={{
         lat: -1.2884,
         lng: 36.8233
        }}
      />
      </div>
      
    );
  }
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyDMVUmkhob_1h73vCYfz9r14H3V6SQFv8o'
})(GoogleMap);