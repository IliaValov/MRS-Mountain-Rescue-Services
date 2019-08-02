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
            message: '',
            users: []
        };

        this.connection = null;
    }

    componentDidMount = () => {

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
            data.map((ar) => this.InitializeUsers(ar));
            console.log(data);

        });

        this.connection.start()
            .then(() => this.connection.invoke("SendUserLocations", "a"))
            .catch(err => console.error('SignalR Connection Error: ', err));
    }

    componentWillUnmount() {
        this.connection.stop();
    }

    InitializeUsers = (user) => {
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

    ShowAllUsers = () => {
        const { users } = this.state;

        console.log(users, "Users");
        console.log(this.state.users, "Users 1.1")
        if (this.state.users.length > 0) {
            console.log(users, "Users in  if")
            return users.map(u => <div className='unselectable'>{u.phonenumber}</div>)
        }
    }

    openNav = () => {
        document.getElementById("mySidenav").style.width = "250px";
    }

    closeNav = () => {
        document.getElementById("mySidenav").style.width = "0";
    }

    getUsersLocations = () => {
        const { users } = this.state;
        
        return users;
    }

    render() {
        return (
            <div>
                <div id="mySidenav" class="sidenav">
                    <a href="javascript:void(0)" class="closebtn" onClick={() => this.closeNav()}>&times;</a>
                    {this.ShowAllUsers()}

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
                        users={this.state.users}
                    />
                </div>
            </div>
        )
    }
}


export default connect()(MapContainer);