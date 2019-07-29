import React from 'react';
import { connect } from 'react-redux';
import GoogleMap from './GoogleMap'
import { HubConnection } from '@aspnet/signalr-client';
import "../../../assets/styles/Map.css";


export class MapContainer extends React.Component {
    constructor(props) {
        super(props);
        
        this.state = {
          message: '',
          locations: [],
          hubConnection: null,
        };
      }

      componentDidMount = () => {
        const hubConnection = new HubConnection('https://localhost:44358/userlocations');
    
        this.setState({ hubConnection}, () => {
          this.state.hubConnection
            .start()
            .then(() => console.log('Connection started!'))
            .catch(err => console.log('Error while establishing connection :('));
    
          this.state.hubConnection.on('SendUserLocations', () => {
          });

          this.state.hubConnection.on('ReceiveUsersWithLastLocation', (receivedLocations) => {
            const locations = this.state.locations.concat([receivedLocations]);
            this.setState({ locations });
        });
        });
      }
      
      ShowAllUsers = () =>{
          return this.state.locations.map(m => {
            <a href="#">{m.phonenumber}</a>
          })
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

                <div className="map-container">
                    <GoogleMap />
                </div>
            </div>
        )
    }
}

export default connect()(MapContainer);