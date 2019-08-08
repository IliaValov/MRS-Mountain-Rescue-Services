import React, { Component } from 'react';
import { Map, GoogleApiWrapper, Marker, Polygon } from 'react-google-maps';

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
      if (users.length === 1) {
        return users.map((u) => {
          if (u.locations || u.locations.length > 0) {
            for (let i = 0; i < u.locations.length; i++) {
              if(i + 1 === u.locations.length){
                return;
              }  

              let locationFirst = u.locations[i];
              let locationSecond = u.locations[i + 1];

              const coords = [{lat: locationFirst.latitude, lng: locationFirst.longitude}, {lat: locationSecond.latitude, lng: locationSecond.longitude}, {lat: locationFirst.latitude, lng: locationFirst.longitude}]

              // return (<Polygon
              //   path={coords}
              //   key={i}
              //   options={{
              //     fillColor: "#000",
              //     fillOpacity: 0.4,
              //     strokeColor: "#000",
              //     strokeOpacity: 1,
              //     strokeWeight: 1
              //   }}
              //   onClick={() => {
              //   }} />)
            }
          }
        })
      } else {
        return users.map((u) => {
          if (u.locations || u.locations.length > 0) {
            return (u.locations.map((l, index) => {
              if (l) {
                return (<Marker key={index}
                  position={{ lat: l.latitude, lng: l.longitude }}
                ></Marker>)
              }
            }))
          }
        })
      }

    }
  }

  render() {
    const coords = [{lat: 42.7017426, lng: 23.2969142}, {lat: 42.701728, lng: 23.2966635},{lat: 42.7018129, lng: 23.2969757}, {lat: 42.7017426, lng: 23.2969142}]


    return (
      <Map
        google={this.props.google}
        zoom={8}
        initialCenter={{
          lat: 42.666748,
          lng: 23.351870
        }}

      >
            <Polygon
        path={[
          {"lat":-83.674464,"lng":41.331119},{"lat":-83.533773,"lng":41.348933},{"lat":-83.534508,"lng":41.297062},{"lat":-83.645375,"lng":41.268504},{"lat":-83.674464,"lng":41.331119}
          ]}
        onRightClick={() => console.log("you clicked ")}
        key={Date.now() + Math.random()}
      />
        {this.showAllLocations()}
      </Map>

    );
  }
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyBWBwtK08PpLeiu2wVEyrr8P7x2LIqSrSk'
})(GoogleMap);