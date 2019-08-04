import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import GoogleMap from './GoogleMap'
import "../../../assets/styles/Map.css";
import { string } from 'prop-types';
import * as signalR from '@aspnet/signalr';
import User from '../../../models/UserModel';
import Location from '../../../models/LocationModel';

export class MapContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: 'ALLUSERS',
            users: []
        };

        this.connection = null;
    }

    componentWillMount() {

        const protocol = new signalR.JsonHubProtocol();

        const options = {
            logMessageContent: true
        };

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl('/userlocations', options)
            .withHubProtocol(protocol)
            .build();

        let a = new User();

        this.connection.on('SendUserLocations', (data) => {
            data.map((ar) => this.initializeUsers(ar));
            console.log(data);

        });

        this.connection.start()
            .then(() => this.connection.invoke("SendUserLocations", "a"))
            .catch(err => console.error('SignalR Connection Error: ', err));
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
                if(u.phonenumber === currentUser){
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
                    <GoogleMap
                        users={this.getUsersLocations()}
                    />
                </div>
            </div>
        )
    }
}


export default connect()(MapContainer);