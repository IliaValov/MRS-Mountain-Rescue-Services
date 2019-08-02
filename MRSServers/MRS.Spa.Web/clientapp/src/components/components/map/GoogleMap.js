import React, { Component } from 'react';
import { Map, GoogleApiWrapper, Marker } from 'google-maps-react';


export class GoogleMap extends Component {
  constructor(props) {
    super(props);

    this.connection = null;
  }

  showAllLocations = () => {

    if (this.probs.users !== undefined) {
      return this.probs.users.map((u) => u.locations.map((l) => {
        <Marker
          position={{ lat: l.latitude, lng: l.longitude }}
        ></Marker>
      }))
    }

  }

  render() {
    return (
      <Map
        google={this.props.google}
        zoom={14}
        initialCenter={{
          lat: 42.666748,
          lng: 23.351870
        }}

      >
        {this.showAllLocations()}
      </Map>

    );
  }
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyBWBwtK08PpLeiu2wVEyrr8P7x2LIqSrSk'
})(GoogleMap);