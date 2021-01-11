import React from "react";
import { Redirect } from "react-router-dom";
import Joi from "joi-browser";
import auth from "../../services/authService";
import { Col, Row } from "reactstrap";
import Form from "../common/form/form";
import { TokenKey } from '../../utils/constant';

class LoginForm extends Form {
  state = {
    data: { email: "", password: "" },
    errors: {},
  };

  schema = {
    email: Joi.string().email().required().label("Email"),
    password: Joi.string().required().label("Password"),
  };

  doSubmit = async () => {
    const { data } = this.state;
    let res = await auth.login(data.email, data.password);
    if (res && res.success && res.data) {
      const { state } = this.props.location;
      localStorage.setItem(TokenKey, res.data);
      window.location = state ? state.from.pathname : "/";
    } else {
      const errors = { ...this.state.errors };
      errors.email = res.message ? res.message : "InValid Credentials";
      this.setState({ errors });
    }
  };

  render() {
    if (auth.getJwt()) return <Redirect to="/" />;

    return (
      <div>
        <div className="">
          <h1>Login</h1>
          <form>
            <Row>
              <Col sm="12" md={{ size: 4, offset: 4 }}>
                {this.renderInput("email", "Email")}
              </Col>
            </Row>
            <Row>
              <Col sm="12" md={{ size: 4, offset: 4 }}>
                {this.renderInput("password", "Password", "password")}
              </Col>
            </Row>
            <Row>
              <Col sm="12" md={{ size: 4, offset: 4 }} className="text-center">
                <button
                  type="button"
                  className="btn btn-success"
                  style={{ padding: "5px 50px" }}
                  onClick={this.handleSubmit}
                >
                  LOGIN
                </button>
              </Col>
            </Row>
          </form>
        </div>
      </div>
    );
  }
}

export default LoginForm;
