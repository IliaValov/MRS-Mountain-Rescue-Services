import React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import Modal from 'react-awesome-modal';
import Login from './user/Login'
import AuthService from '../actions/AuthService';
import './NavMenu.css';

export default class NavMenu extends React.Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false,
      isLoginVisiable: false,
      authService: new AuthService()
    };
  }
  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }

  openModal = () => {
    this.setState({
      isLoginVisiable: true
    });
  }

  handleLogoutClick = () => {
    this.state.authService.logout();
  }

  closeModal = () => {
    this.setState({
      isLoginVisiable: false
    });
  }


  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow" light >
          <Container>
            <NavbarBrand tag={Link} to="/">MRS.Spa.Web</NavbarBrand>
            <NavbarToggler onClick={this.toggle} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/map">Map</NavLink>
                </NavItem>
                {!this.state.authService.isAuthenticated() ?
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/login" >Login</NavLink>
                  </NavItem>
                  : null}
                {this.state.authService.isAuthenticated() ?
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/" onClick={() => this.handleLogoutClick()}>Logout</NavLink>
                  </NavItem>
                  : null}
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      
      </header>
    );
  }
}
