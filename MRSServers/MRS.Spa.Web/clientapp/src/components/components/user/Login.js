import React from 'react';
import { connect } from 'react-redux';
import '../../../assets/styles/Login.css'

export class Login extends React.Component {


    render() {
        return (
            <div class="login-page">
                <div class="form">    
                    <form class="login-form">
                        <input type="text" placeholder="username" />
                        <input type="password" placeholder="password" />
                        <button>login</button>
                    </form>
                </div>
            </div>
        )
    }
}

export default connect()(Login);