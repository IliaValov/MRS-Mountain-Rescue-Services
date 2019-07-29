import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import GoogleMap from './GoogleMap'
import "../../../assets/styles/Map.css";
import { string } from 'prop-types';
import * as signalR from '@aspnet/signalr';


export class MapContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            message: '',
            locations: [],
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

        this.connection.on('SendUserLocations', (data) => {
            const locations = this.state.locations.concat([data]);
            this.setState({ locations })
        });

        this.connection.start()
            .then(() => this.connection.invoke("SendUserLocations", "a"))
            .catch(err => console.error('SignalR Connection Error: ', err));

        
    }

    componentWillUnmount() {
        this.connection.stop();
    }

    ShowAllUsers = () => {
        const {locations} = this.state;
        if(locations.length > 0){
            return locations.map(u => u.map(u =>  <a href="#">{u.phoneNumber}</a>)
               
            )
        }
    }



    openNav = () => {
        document.getElementById("mySidenav").style.width = "250px";
    }

    closeNav = () => {
        document.getElementById("mySidenav").style.width = "0";
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

                {/* <div className="map-container">
                    <GoogleMap />
                </div> */}
            </div>
        )
    }
}


export default connect()(MapContainer);