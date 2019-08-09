import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import "../../assets/styles/Map.css";
import { string } from 'prop-types';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import User from '../../models/UserModel';
import Location from '../../models/LocationModel';
import { compose, withProps } from "recompose"
import { withScriptjs, withGoogleMap, GoogleMap, Marker, Polygon } from "react-google-maps"

const MyMapComponent = compose(
    withProps({
        googleMapURL: "https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places",
        loadingElement: <div style={{ height: `100%` }} />,
        containerElement: <div style={{ height: `90%` }} />,
        mapElement: <div style={{ height: `100%` }} />,
    }),
    withScriptjs,
    withGoogleMap
)((props) =>
    <GoogleMap
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

    if (users) {

        return users.map((u) => {
            if (u.locations || u.locations.length > 0) {
                return (u.locations.map((l, index) => {
                    if (l) {
                        return (<Marker key={index}
                            position={{ lat: l.latitude, lng: l.longitude }}
                            title={ 'Phone number: ' + u.phonenumber + '\r\n' + 'lat: ' + l.latitude + ', lng: ' + l.longitude}
                        ></Marker>)
                    }
                }))
            }
        })
    }
}


export class MapContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: 'ALLUSERS',
            users: [],
            HubConnection: null
        };

        this.connection = null;
    }

    componentWillMount() {
        const { HubConnection } = this.state;

        var dateOptions = { year: 'numeric', month: 'numeric', day: 'numeric' };

        const protocol = new signalR.JsonHubProtocol();

        const options = {
            logMessageContent: true
        };

        let hub = this.connection = new signalR.HubConnectionBuilder()
            .withUrl('/userlocations', options)
            .withHubProtocol(protocol)
            .build();

        hub.on('SendUserLocations', (data) => {
            data.map((ar) => this.initializeUsers(ar));
            console.log(data);

        });

        hub.start()
            .then(() => this.connection.invoke("SendUserLocations", new Date().toLocaleDateString([], dateOptions).toString()))
            .catch(err => console.error('SignalR Connection Error: ', err));

        this.setState({ HubConnection: hub });
    }

    componentWillUnmount() {
        this.connection.stop();
    }

    initializeUsers = (user) => {
        const { users } = this.state;

        let userArray = []

        let currentUser = new User();

        currentUser.phonenumber = user.phoneNumber;

        user.locations.map(l => {
            let location = new Location();

            location.latitude = l.latitude;
            location.longitude = l.longitude;
            location.altitude = l.altitude;

            currentUser.locations.push(location);
        })

        this.setState(state => {
            const list = state.users.push(currentUser);

            return list;
        })

        console.log(this.state.users)
        this.forceUpdate();
    }

    handleChangeCurrentUser = (event) => {
        let a = event.target.textContent;

        this.setState({ currentUser: a });
    }

    showAllUsers = () => {
        const { users } = this.state;

        console.log(users, "Users");
        console.log(this.state.users, "Users 1.1")
        if (this.state.users.length > 0) {
            console.log(users, "Users in  if")
            return users.map(u => <div className='unselectable' value={u.phoneNumber} onClick={(e) => this.handleChangeCurrentUser(e)} >{u.phonenumber}</div>)
        }
    }

    openNav = () => {
        document.getElementById("mySidenav").style.width = "250px";
    }

    closeNav = () => {
        document.getElementById("mySidenav").style.width = "0";
    }

    getUsersLocations = () => {
        const { users, currentUser } = this.state;

        if (currentUser !== "ALLUSERS") {
            let user = [];

            users.map((u) => {
                console.log(u);
                console.log(u.phonenumber);
                if (u.phonenumber === currentUser) {
                    console.log("DONE");
                    user.push(u);
                }
            })

            return user;
        }

        return this.getUsersLastLocation();
    }

    getUsersLastLocation = () => {
        const { users } = this.state;

        let resultUsers = [];

        users.map((u) => {
            let user = new User();
            user.phonenumber = u.phonenumber;

            user.locations.push(u.locations[u.locations.length - 1]);

            resultUsers.push(user);
        });

        return resultUsers
    }

    render() {
        return (
            <div>
                <div>{this.state.currentUser}</div>
                <div id="mySidenav" class="sidenav">
                    <a href="javascript:void(0)" class="closebtn" onClick={() => this.closeNav()}>&times;</a>
                    <div className='unselectable' onClick={(e) => this.handleChangeCurrentUser(e)} >ALLUSERS</div>
                    {this.showAllUsers()}

                    {/* <a href="#">0888014990</a>
                    <a href="#">Services</a>
                    <a href="#">Clients</a>
                    <a href="#">Contact</a> */}
                </div>

                <div className="nav-bar-vertical">
                    <ul className="nav-bar-vertical">
                        <li className="unselectable" onClick={() => this.openNav()}>
                            Users
                        </li>
                        <li className="unselectable">
                            Polygons
                        </li>
                        <li className="unselectable">
                            Logs
                        </li>
                    </ul>
                </div>

                <div className="map-container">
                    <MyMapComponent
                        users={this.getUsersLocations()}
                    />
                </div>
            </div>
        )
    }
}


export default connect()(MapContainer);