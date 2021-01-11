import React from "react";
import { Component } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router";
import "../css/home.css";
import authService from "../services/authService";
import Promos from "./promo/Promos";

class Home extends Component {
  state = {};

  render() {
    return (
      <div>
        {!authService.getJwt() && <Redirect to="/login" />}
        {authService.getJwt() &&
          <div>
            <Promos {...this.props} />
          </div>
        }
      </div>
    )
  };
}

export default connect()(Home);
