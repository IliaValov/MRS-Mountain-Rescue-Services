import React from 'react';
import { connect } from 'react-redux';


import "../assets/styles/Map.css";
import AuthService from '../actions/AuthService';

var authservice = new AuthService();

const Home = props => (
  <div>
    <h1>Welcome to Moutain rescue services</h1>
    <ul>
      <li><a href='https://github.com/IliaValov/MRS-Mountain-Rescue-Services'>Link to the github</a></li>
    </ul>

    <h3>This websites is only for administrators if you are not please leave</h3>


  </div>
);

export default connect()(Home);
