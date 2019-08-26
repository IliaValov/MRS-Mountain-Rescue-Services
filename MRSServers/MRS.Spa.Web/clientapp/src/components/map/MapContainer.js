import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import "../../assets/styles/Map.css";
import { string } from 'prop-types';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import User from '../../models/UserModel';
import Location from '../../models/LocationModel';
import { compose, withProps } from "recompose";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import { withScriptjs, withGoogleMap, GoogleMap, Marker, Polygon } from "react-google-maps";

const MyMapComponent = compose(
    withProps({
        googleMapURL: "https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places",
        loadingElement: <div style={{ height: `100%` }} />,
        containerElement: <div style={{ height: `100%` }} />,
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
                            title={'Phone number: ' + u.phoneNumber + '\r\n' + 'lat: ' + l.latitude + ', lng: ' + l.longitude}
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
            currentUser: new User(),
            currentDate: '',
            findUsers: '',
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
            this.setState({ users: [] });
            console.log(data)
            data.map((ar) => this.initializeUsers(ar));
            console.log(data);

        });

        let date = new Date();

        console.log(date);

        let currentDate = date.getMonth() + 1 + '/' + date.getDate() + '/' + date.getFullYear();

        hub.start()
            .then(() => this.connection.invoke("SendUserLocations", currentDate))
            .catch(err => console.error('SignalR Connection Error: ', err));

        this.setState({ HubConnection: hub, currentDate: currentDate });

        setInterval(async () => this.CheckForUpdate(), 5000);
    }

    CheckForUpdate = async () => {
        const { currentDate } = this.state;
        console.log("done")
        this.connection.invoke("SendUserLocations", currentDate)
    }

    componentWillUnmount() {
        this.connection.stop();
    }

    initializeUsers = (user) => {
        const { users } = this.state;

        let userArray = [];

        let currentUser = new User();

        currentUser = user;

        this.setState(state => {
            const list = state.users.push(currentUser);

            return list;
        })

        // this.CheckForUpdate();

        console.log(this.state.users)
        this.forceUpdate();
    }

    handleChangeCurrentUser = (event) => {
        const { users } = this.state;
        let selectedOption = event.target.textContent;


        if (selectedOption !== "ALLUSERS") {
            let selectedUser = users.find(x => x.phoneNumber);

            this.setState({ currentUser: selectedUser });

            this.openUserInfo();
        } else {
            this.setState({ currentUser: new User() });
        }


    }

    showAllUsers = () => {
        const { users, findUsers } = this.state;

        console.log(users, "Users");
        console.log(this.state.users, "Users 1.1")
        if (this.state.users.length > 0) {
            console.log(users, "Users in  if")
            return users.map((u, index) => {
                if (findUsers === '') {
                    return (<div key={index} className='unselectable' value={u.phoneNumber} onClick={(e) => this.handleChangeCurrentUser(e)} >
                        {u.phoneNumber}
                    </div>)
                }
                if (u.phoneNumber.includes(findUsers)) {
                    return (<div key={index} className='unselectable' value={u.phoneNumber} onClick={(e) => this.handleChangeCurrentUser(e)} >
                        {u.phoneNumber}
                    </div>)
                }
            })
        }
    }


    openNav = () => {
        document.getElementById("userMenu").style.width = "250px";

    }

    closeNav = () => {
        document.getElementById("userMenu").style.width = "0";
    }

    openUserInfo = () => {
        document.getElementById("popup").style.width = "20%";
    }

    closeUserInfo = () => {
        document.getElementById("popup").style.width = "0";
    }

    getUsersLocations = () => {
        const { users, currentUser } = this.state;

        if (currentUser.phoneNumber) {
            let user = [];

            users.map((u) => {
                console.log(u);
                console.log(u.phoneNumber);
                if (u.phoneNumber === currentUser.phoneNumber) {
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
            user.phoneNumber = u.phoneNumber;

            user.locations.push(u.locations[u.locations.length - 1]);

            resultUsers.push(user);
        });

        return resultUsers
    }

    hnadleDatePickerChange = (startDate) => {
        let newDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();

        this.setState({ currentDate: newDate })
    }

    handleUserInputChange = (event) => {
        this.setState({ findUsers: event.target.value });
    }

    render() {
        const { currentDate, findUsers, currentUser } = this.state;

        return (
            <div>
                <DatePicker
                    selected={currentDate != '' ? new Date(currentDate) : new Date()}
                    onChange={this.hnadleDatePickerChange}
                />
                <div id="userMenu" className="sidenav">
                    <a href="javascript:void(0)" className="closebtn" onClick={() => this.closeNav()}>&times;</a>
                    <input type="text" value={findUsers} onChange={this.handleUserInputChange} />
                    <div className='unselectable' onClick={(e) => this.handleChangeCurrentUser(e)} >ALLUSERS</div>
                    {this.showAllUsers()}

                    {/* <a href="#">0888014990</a>
                    <a href="#">Services</a>
                    <a href="#">Clients</a>
                    <a href="#">Contact</a> */}
                </div>

                <div id="popup" className="popup">
                    <a href="javascript:void(0)" className="closebtn" onClick={() => this.closeUserInfo()}>&times;</a>
                    <div>
                        Phone number: {currentUser.phoneNumber}
                    </div>
                    <div>
                        Message: {currentUser.messages.length > 0 ? currentUser.messages[currentUser.messages.length - 1].message : ""}
                    </div>
                    <div>
                        Condition: {currentUser.messages.length > 0 ? currentUser.messages[currentUser.messages.length - 1].condition : ""}
                    </div>
                    <div>
                        Is in danger: {currentUser.isInDanger ? "Yes" : "No"}
                    </div>
                    <div>
                        Type: {currentUser.userType}
                    </div>
                    {currentUser.isInDanger ?
                        <div className='log-button'>
                            Log mission
                        </div> : null}
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