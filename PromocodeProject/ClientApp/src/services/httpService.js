import axios from "axios";
import { app, error500, netwrokError, login } from "../routes.json";

const tokenKey = "token";
axios.interceptors.response.use(null, (error) => {
  if (!error.status && error.message === "Network Error") {
    // Log this error somewhere
    return (window.location = app + netwrokError);
  } else if (
    error.response &&
    error.response.status &&
    error.response.status >= 500 &&
    error.response.status <= 599
  ) {
    // Log this error somewhere
    window.location = app + error500;
  } else if (
    error.response &&
    error.response.status &&
    error.response.status === 401 &&
    !error.config.url.includes("login")
  ) {
    localStorage.removeItem(tokenKey);
    window.location = app + login;
  } else if (error.response && error.response.data) {
    return Promise.reject(error);
  } else {
    // Something wrong
    // Log this error somewhere
    window.location = app + error500;
  }
});

async function post(url, data) {
  return axios
    .post(url, data)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        return err.response.data;
      }
    });
}

async function put(url, data) {
  return axios
    .put(url, data)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        return err.response.data;
      }
    });
}

async function get(url) {
  return axios
    .get(url)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        return err.response.data;
      }
    });
}

async function patch(url) {
  return axios
    .patch(url)
    .then((res) => {
      return res.data;
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        return err.response.data;
      }
    });
}

function setJwt(jwt) {
  axios.defaults.headers.common["Content-Type"] = "application/json";
  delete axios.defaults.headers.common["Authorization"];
  if (jwt && jwt !== "undefined") {
    axios.defaults.headers.common["Authorization"] = `bearer ${jwt}`;
  } else {
    delete axios.defaults.headers.common["Authorization"];
  }
}

export default {
  post: post,
  get: get,
  put: put,
  setJwt: setJwt,
  patch: patch,
};
