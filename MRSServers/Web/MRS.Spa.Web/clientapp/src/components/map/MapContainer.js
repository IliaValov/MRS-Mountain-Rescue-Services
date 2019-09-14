import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import MissionLogService from "../../actions/MissionLogService";
import "../../assets/styles/Map.css";
import { string } from 'prop-types';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import User from '../../models/UserModel';
import MissionLogModel from '../../models/MissionLogModel';
import Location from '../../models/LocationModel';
import { compose, withProps } from "recompose";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import { withScriptjs, withGoogleMap, GoogleMap, Marker, Polygon } from "react-google-maps";
import { MarkerWithLabel } from 'react-google-maps/lib/components/addons/MarkerWithLabel';
import AuthService from '../../actions/AuthService';

const MyMapComponent = compose(
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


export class MapContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: new User(),
            currentDate: '',
            findUsers: '',
            users: [],

            missionLogs: [],
            currentMissionLog: new MissionLogModel(),
            findMissionLogByName: '',

            missionLog: new MissionLogModel(),
            missionLogService: new MissionLogService(),
            authservice: new AuthService(),

            intervalId: null,
            HubConnection: null
        };

        this.connection = null;
    }

 componentWillMount() {
        const { HubConnection, authservice } = this.state;

        var dateOptions = { year: 'numeric', month: 'numeric', day: 'numeric' };

        if(!authservice.isAuthenticated()){
            this.props.history.push('/login');
            return;
        }

        const protocol = new signalR.JsonHubProtocol();

        const options = {
            logMessageContent: true,
            accessTokenFactory:() => authservice.getToken()
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
            .catch(err => console.error('SignalR Connection Error: ', err)), 5000;

        let intervalId = setInterval(async () => this.CheckForUpdate(), 5000);

        setTimeout(async () => await this.getAllMissionLogs(), 50);

        this.setState({ HubConnection: hub, currentDate: currentDate, intervalId: intervalId });
    }

    componentWillUnmount() {
        clearInterval(this.state.intervalId)
    }

    getAllMissionLogs = async () => {
        const { missionLogService } = this.state;

        let missionLogs = await missionLogService.getAllMissionLogs();

        console.log("Mission logs");
        console.log(missionLogs)

        this.setState({ missionLogs: missionLogs });
    }

    CheckForUpdate = async () => {
        const { currentDate, isConnectionStarted } = this.state;
        console.log("done")

        this.connection.invoke("SendUserLocations", currentDate)

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
            let selectedUser = users.find(x => x.phoneNumber === selectedOption);

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
        this.closeMissionLogMenu();
    }

    closeNav = () => {
        document.getElementById("userMenu").style.width = "0";
    }

    openUserInfo = () => {
        document.getElementById("user-info").style.width = "20%";
    }

    closeUserInfo = () => {
        document.getElementById("user-info").style.width = "0";
    }

    openLogMission = () => {
        document.getElementById("log-mission").style.width = "40%";
    }

    closeLogMission = () => {
        document.getElementById("log-mission").style.width = "0";
        this.setState({ missionLog: new MissionLogModel() })
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
            user.isInDanger = u.isInDanger;
            user.userType = u.userType;
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

    handleMissionLogInputChange = (event) => {
        this.setState({ findMissionLogByName: event.target.value });
    }

    handleCreateMissionLog = () => {
        const { missionLog, missionLogService, currentUser } = this.state;

        if (missionLog) {
            if (!missionLog.missionName) {
                alert("Add mission name!");
                return;
            } else if (!missionLog.details) {
                alert("Add details!");
                return;
            }
            else {
                missionLog.phoneNumber = currentUser.phoneNumber;
                missionLogService.addMissionLog(missionLog);
                this.getAllMissionLogs();
                this.closeLogMission();
            }
        }

        this.setState({ missionLog: new MissionLogModel() });
    }

    openMissionLogMenu = () => {
        document.getElementById("missionLogMenu").style.width = "250px";
        this.closeNav();
    }

    closeMissionLogMenu = () => {
        document.getElementById("missionLogMenu").style.width = "0";
    }

    handleChangeCurrentMissionLog = (missionLog) => {
        this.showCurrentLogMission();
        this.setState({ currentMissionLog: missionLog });
    }

    showAllMissionLogs = () => {
        const { missionLogs, findMissionLogByName } = this.state;

        return missionLogs.map((currentMissionLog, index) => {
            if (findMissionLogByName === "") {
                return (
                    <div key={index} className='unselectable' value={currentMissionLog.missionName} onClick={(e) => this.handleChangeCurrentMissionLog(currentMissionLog)} >
                        {currentMissionLog.missionName}
                    </div>)
            } else {
                if (currentMissionLog.missionName.toLowerCase().includes(findMissionLogByName.toLowerCase())) {
                    return (
                        <div key={index} className='unselectable' value={currentMissionLog.missionName} onClick={(e) => this.handleChangeCurrentMissionLog(currentMissionLog)} >
                            {currentMissionLog.missionName}
                        </div>)
                }
            }


        })
    }

    showCurrentLogMission = () => {
        document.getElementById("current-log-mission").style.width = "20%";
    }

    closeCurrentLogMission = () => {
        document.getElementById("current-log-mission").style.width = "0";
    }

    render() {
        const { currentDate, findUsers, currentUser, missionLog, currentMissionLog, findMissionLogByName } = this.state;

        return (
            <div>
                <DatePicker
                    selected={currentDate != '' ? new Date(currentDate) : new Date()}
                    onChange={this.hnadleDatePickerChange}
                />
                <div id="userMenu" className="sidenav">
                    <a href="javascript:void(0)" className="closebtn" onClick={() => this.closeNav()}>&times;</a>
                    <input type="text" placeholder={"*Enter phone number"} value={findUsers} onChange={this.handleUserInputChange} />
                    <div className='unselectable' onClick={(e) => this.handleChangeCurrentUser(e)} >ALLUSERS</div>
                    {this.showAllUsers()}

                    {/* <a href="#">0888014990</a>
                    <a href="#">Services</a>
                    <a href="#">Clients</a>
                    <a href="#">Contact</a> */}
                </div>

                <div id="missionLogMenu" className="sidenav">
                    <a href="javascript:void(0)" className="closebtn" onClick={() => this.closeMissionLogMenu()}>&times;</a>
                    <input type="text" placeholder={"*Enter mission name"} value={findMissionLogByName} onChange={this.handleMissionLogInputChange} />
                    {this.showAllMissionLogs()}

                    {/* <a href="#">0888014990</a>
                    <a href="#">Services</a>
                    <a href="#">Clients</a>
                    <a href="#">Contact</a> */}
                </div>

                <div id="user-info" className="popup-user-info">
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
                        <div className='log-button' onClick={() => this.openLogMission()}>
                            Log mission
                        </div> : null}
                </div>

                <div id="log-mission" className="popup-log-mission">
                    <a href="javascript:void(0)" className="closebtn" onClick={() => this.closeLogMission()}>&times;</a>
                    <div className="item">
                        PhoneNumber: {currentUser.phoneNumber}
                    </div>
                    <div className="item">
                        Mission name: <input value={missionLog.missionName}
                            onChange={(e) => {
                                let tempMissionLog = missionLog;
                                tempMissionLog.missionName = e.target.value;
                                this.setState({ missionLog: tempMissionLog })
                                this.forceUpdate()
                            }}></input>
                    </div>
                    <div className="item">
                        Completed:
                        <div class="dropdown">
                            <button class="dropbtn">{missionLog.isMissionSuccess ? "YES" : "NO"}</button>
                            <div class="dropdown-content">
                                <div className="item" onClick={() => { missionLog.isMissionSuccess = true; this.setState({ missionLog }); this.forceUpdate() }}>Yes</div>
                                <div className="item" onClick={() => { missionLog.isMissionSuccess = false; this.setState({ missionLog }); this.forceUpdate() }}>No</div>
                            </div>
                        </div>
                    </div>
                    <div className="item">
                        Details:
                        <br />
                        <textarea className="details"
                            value={missionLog.details}
                            onChange={(e) => {
                                let tempMissionLog = missionLog;
                                tempMissionLog.details = e.target.value;
                                this.setState({ missionLog: tempMissionLog })
                                this.forceUpdate()
                            }}>

                        </textarea>
                    </div>
                    <div className="submitbut" onClick={() => this.handleCreateMissionLog()}>Submit</div>
                </div>


                <div id="current-log-mission" className="popup-current-log-mission">
                    <a href="javascript:void(0)" className="closebtn" onClick={() => this.closeCurrentLogMission()}>&times;</a>
                    <div className="item">
                        PhoneNumber: {currentMissionLog.phoneNumber}
                    </div>
                    <div className="item">
                        Mission name:
                            {currentMissionLog.missionName}
                    </div>
                    <div className="item">
                        Completed:
                            {currentMissionLog.isMissionSuccess ? "YES" : "NO"}
                    </div>
                    <div className="item">
                        Details:
                        <br />
                        {currentMissionLog.details}
                    </div>
                </div>

                <div className="nav-bar-vertical">
                    <ul className="nav-bar-vertical">
                        <li className="unselectable" onClick={() => this.openNav()}>
                            Users
                        </li>
                        <li className="unselectable">
                            Polygons
                        </li>
                        <li className="unselectable" onClick={() => this.openMissionLogMenu()}>
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