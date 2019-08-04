import React, { Component } from 'react';
import { Map, GoogleApiWrapper, Marker } from 'google-maps-react';
import User from '../../../models/UserModel';


export class GoogleMap extends Component {
  constructor(props) {
    super(props);

    this.state = {
      users: this.props.users
    }

    this.connection = null;
  }

  componentDidUpdate(prevProps) {
    if (this.props.users !== prevProps.users) {
      this.setState({ users: this.props.users })
    }

  }

  showAllLocations = () => {

    const { users } = this.state;

    if (users) {
      return users.map((u) => {
        return (u.locations.map((l, index) => {
          return (<Marker key={index}
            position={{ lat: l.latitude, lng: l.longitude }}
          ></Marker>)
        }))
      })


    }
  }

  render() {
    return (
      <Map
        google={this.props.google}
        zoom={8}
        initialCenter={{
          lat: 42.666748,
          lng: 23.351870
        }}

      >
        <Marker
          position={{ lat: 42.666748, lng: 23.351870 }}
        ></Marker>

        {this.showAllLocations()}
      </Map>

    );
  }
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyBWBwtK08PpLeiu2wVEyrr8P7x2LIqSrSk'
})(GoogleMap);