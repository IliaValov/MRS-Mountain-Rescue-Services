import React, { Component } from 'react';
import { connect } from 'react-redux';
import '../../assets/styles/Login.css'
import { withRouter } from 'react-router-dom'
import { push } from 'react-router-redux'
import AuthService from '../../actions/AuthService';

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            isSuccess: true,
            authService: new AuthService()
        }
    }

    handleLoginButton = async () => {
        const { username, password, authService } = this.state;
        let isLogged = await this.state.authService.loginUser(username, password);

        if (isLogged) {
            this.props.history.push('/');
        } else {
            this.setState({ isSuccess: false })
        }


    }

    handleOnChangeUsername = (event) => {
        this.setState({ username: event.target.value })
    }

    handleOnChangePassword = (event) => {
        this.setState({ password: event.target.value })
    }

    handleSubmitForm = () => {
        this.props.history.push('/'); // navigate to some route
    }

    render() {
        return (
            <div id="login-page" className="login-page">
                {this.state.isSuccess ? null : <div>Wrong username or password</div>}

                <div className="form">
                    <input type="text" onChange={this.handleOnChangeUsername} placeholder="username" />
                    <input type="password" onChange={this.handleOnChangePassword} placeholder="password" />
                    <button onClick={() => this.handleLoginButton()}>login</button>
                </div>
            </div>
        )
    }
}

export default withRouter(connect()(Login));