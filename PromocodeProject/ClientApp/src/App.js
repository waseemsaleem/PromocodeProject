import React from "react";
import { Route } from "react-router";
import Layout from "./components/Layout";
import Home from "./components/Home";
import Counter from "./components/Counter";
import FetchData from "./components/FetchData";
import LoginForm from "./components/security/login";
import Error404 from "./components/security/error404";
import Error500 from "./components/security/error500";
import NetworkError from "./components/security/networkError";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export default () => (
  <Layout>
    <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
      {/* Same as */}
      <ToastContainer />
    <Route exact path="/" component={Home} />{" "}
    <Route path="/counter" component={Counter} />{" "}
    <Route path="/fetchdata/:startDateIndex?" component={FetchData} />{" "}
    <Route path="/login" component={LoginForm} />
    <Route path="/error404" component={Error404} />
    <Route path="/error500" component={Error500} />
    <Route path="/network-error" component={NetworkError} />
  </Layout>
);
