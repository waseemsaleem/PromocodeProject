import React, { Component } from 'react';
import { Navbar, Row, Col, Grid, Button } from "react-bootstrap";
import authService from "../services/authService";

class NavBar extends Component {
  state = {  }

  logout = () => {
    authService.removeToken();
    window.location = "/";
  }

  render() { 
    return ( 
      <Navbar fixed="top" bg="light" variant="light" className="topSidebar">
        <Grid>
          <Row className="Navbar">
            <Col sm={2}>
              <div className="navText">
                <p>Balance</p>
                <h3>213 920&#36;</h3>
              </div>
            </Col>
            <Col sm={2}>
              <div className="navText">
                <p>Payout</p>
                <h3>159 456&#36;</h3>
              </div>
            </Col>
            {authService.getJwt() &&
              <Col sm={2} smOffset={6}>
                <Button variant="outline-primary" onClick={this.logout}>Logout</Button>{' '}
              </Col>
            }

          </Row>
        </Grid>
      </Navbar>
     );
  }
}
 
export default NavBar;