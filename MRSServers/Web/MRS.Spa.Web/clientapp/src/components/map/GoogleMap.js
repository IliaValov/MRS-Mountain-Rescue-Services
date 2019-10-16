import React, { PureComponent } from 'react';
import "../../assets/styles/Map.css";
import { compose, withProps } from "recompose";
import { withScriptjs, withGoogleMap, GoogleMap, Marker, Polygon } from "react-google-maps";

export const GoogleMapComponent = compose(
  withProps({
      googleMapURL: "https://maps.googleapis.com/maps/api/js?key=AIzaSyBWBwtK08PpLeiu2wVEyrr8P7x2LIqSrSk&v=3.exp&libraries=geometry,drawing,places",
      loadingElement: <div style={{ height: `100%` }} />,
      containerElement: <div style={{ height: `100%` }} />,
      mapElement: <div style={{ height: `100%` }} />,
  }),
  withScriptjs,
  withGoogleMap
)((props) =>
  <GoogleMap
      bootstrapURLKeys={{ key: "AIzaSyBWBwtK08PpLeiu2wVEyrr8P7x2LIqSrSk" }}
      defaultZoom={8}
      defaultCenter={{ lat: 44.666829, lng: 22.352099 }}
  >
      {showUserTrace(props.users)}
      {showAllLocations(props.users)}
  </GoogleMap>
)

function showUserTrace(users) {
  if (users.length === 1) {
      return users.map((u) => {
          return u.locations.map((l, i) => {
              if (i + 1 === u.locations.length) {
                  return;
              }

              let locationFirst = u.locations[i];
              let locationSecond = u.locations[i + 1];

              const coords = [{ lat: locationFirst.latitude, lng: locationFirst.longitude }, { lat: locationSecond.latitude, lng: locationSecond.longitude }]

              return (<Polygon
                  path={coords}
                  key={i}
              />)
          })
      })
  }
}

function showAllLocations(users) {
  let saviorIcon = new window.google.maps.MarkerImage(
      "http://maps.google.com/mapfiles/ms/icons/blue.png",
      null, /* size is determined at runtime */
      null, /* origin is 0,0 */
      null, /* anchor is bottom center of the scaled image */
      new window.google.maps.Size(32, 32)
  );
  let normalUserIcon = new window.google.maps.MarkerImage(
      "http://maps.google.com/mapfiles/ms/icons/green.png",
      null, /* size is determined at runtime */
      null, /* origin is 0,0 */
      null, /* anchor is bottom center of the scaled image */
      new window.google.maps.Size(32, 32)
  );
  let emergencyIcon = new window.google.maps.MarkerImage(
      "http://maps.google.com/mapfiles/ms/icons/red.png",
      null, /* size is determined at runtime */
      null, /* origin is 0,0 */
      null, /* anchor is bottom center of the scaled image */
      new window.google.maps.Size(32, 32)
  );
  if (users) {

      return users.map((u) => {
          if (u.locations || u.locations.length > 0) {
              if (u.isInDanger) {
                  //Emergency user
                  return (u.locations.map((l, index) => {
                      if (l) {
                          return (<Marker key={index}
                              icon={emergencyIcon}
                              position={{ lat: l.latitude, lng: l.longitude }}
                              title={'Phone number: ' + u.phoneNumber + '\r\n' + 'lat: ' + l.latitude + ', lng: ' + l.longitude+ ", createdOn: " + l.createdOn}
                          ></Marker>)
                      }
                  }))
              } else if (u.userType.toLowerCase() === "savior") {
                  //Savior user
                  return (u.locations.map((l, index) => {
                      if (l) {
                          return (<Marker key={index}
                              icon={saviorIcon}
                              position={{ lat: l.latitude, lng: l.longitude }}
                              title={'Phone number: ' + u.phoneNumber + '\r\n' + 'lat: ' + l.latitude + ', lng: ' + l.longitude + ", createdOn: " + l.createdOn}
                          ></Marker>)
                      }
                  }))
              } else {
                  //Normal user
                  return (u.locations.map((l, index) => {
                      if (l) {
                          return (<Marker key={index}
                              icon={normalUserIcon}
                              position={{ lat: l.latitude, lng: l.longitude }}
                              title={'Phone number: ' + u.phoneNumber + '\r\n' + 'lat: ' + l.latitude + ', lng: ' + l.longitude+ ", createdOn: " + l.createdOn}
                          ></Marker>)
                      }
                  }))
              }
          }
      })
  }
}
