import React, { Component } from 'react';
import { connect } from 'react-redux';
import '../../../assets/styles/Login.css'
import { withRouter } from 'react-router-dom'
import { push } from 'react-router-redux'
import AuthService from '../../../actions/AuthService';

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            authService: new AuthService()
        }
    }

    handleLoginButton = () => {
        const { username, password, authService } = this.state;
        console.log(this.state.authService.loginUser('MRSAdministrator', 'MrsAdministrator123'));
    }

    handleOnChangeUsername = (event) => {
        this.setState({ username: event.target.value })
    }

    handleOnChangePassword = (event) => {
        this.setState({ password: event.target.value })
    }

    handleSubmitForm = () => {
        push('/'); // navigate to some route
    }

    render() {
        return (
            <div class="login-page">
                <div class="form">
                        <input type="text" onChange={this.handleOnChangeUsername} placeholder="username" />
                        <input type="password" onChange={this.handleOnChangePassword} placeholder="password" />
                        <button onClick={() => this.handleLoginButton()}>login</button>
                </div>
            </div>
        )
    }
}

export default withRouter(connect()(Login));