import jwtDecode from 'jwt-decode';


export class AuthService {

    loginUserSuccess(token) {
        localStorage.setItem('token', token);
    }

    setToken(authResult) {
        // Saves user token to localStorage    
        localStorage.setItem('id_token', authResult.access_token);
        let expiresAt = JSON.stringify((authResult.expires_in * 1000) + new Date().getTime());
        localStorage.setItem('expires_at', expiresAt);
    }

    getToken(token) {
        return localStorage.getItem('id_token', token);
    }

    loginUser(username, password, redirect = "/") {

        var details = {
            'username': username,
            'password': password
        };

        var formBody = [];
        for (var property in details) {
            var encodedKey = encodeURIComponent(property);
            var encodedValue = encodeURIComponent(details[property]);
            formBody.push(encodedKey + "=" + encodedValue);
        }
        formBody = formBody.join("&");

        return fetch('https://localhost:44358/api/account/login', {
            method: 'post',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: formBody
        })
            .then(res => res.json())
            .then(response => {
                try {
                    console.log(JSON.stringify(response.access_token));
                    if (response.access_token) {
                        this.setToken(response);
                    }
                } catch (e) {
                    console.log(e);
                }
            })
            .catch(error => {
                Promise.reject(error);
            })
    }


    logout() {
        // Clear access token and ID token from local storage    
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
        // navigate to the home route
        //history.replace('/feed');
        //window.location.reload();
    }

    isAuthenticated() {
        try {
            const token = this.getToken();
            const expires = localStorage['expires_at'];

            if (token && token !== "undefined" && expires) {
                let expiresAt = JSON.parse(expires);
                return new Date().getTime() < expiresAt;
            }

            return false;
        }
        catch (err) {
            return false;
        }
    }
}

export default AuthService