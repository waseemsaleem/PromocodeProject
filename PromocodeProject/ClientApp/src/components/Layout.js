import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './NavMenu';
import NavBar from './NavBar';
import '../css/navbar.css'

export default props => (
  <Grid fluid>
    <Row>
      <Col sm={1}>
        <NavMenu />
      </Col>
      <Col sm={11}>
        <Row className="topNavbar">
          <Col sm={12}>
            <NavBar />
          </Col>
        </Row>
        {props.children}
      </Col>
    </Row>
  </Grid>
);
