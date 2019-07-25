import React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import Modal from 'react-awesome-modal';
import Login from './components/user/Login'
import './NavMenu.css';

export default class NavMenu extends React.Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false,
      isLoginVisiable: false
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
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/" onClick={() => this.openModal()}>Login</NavLink>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
        <Modal visible={this.state.isLoginVisiable} width="360px" height="250.4px" effect="fadeInUp" onClickAway={() => this.closeModal()}>
          <Login/>
        </Modal>
      </header>
    );
  }
}
