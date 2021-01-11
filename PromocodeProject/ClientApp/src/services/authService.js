import http from "./httpService";
import {
  baseUrl,
  loginUrl,
  jwtTokenControllerUrl,
} from "../api.json";
import { TokenKey } from '../utils/constant';

const controllerUrl = baseUrl + jwtTokenControllerUrl;

http.setJwt(getJwt());

export async function login(email, password) {
  const url = controllerUrl + loginUrl;
  return await http.post(url, {
    email,
    password,
  });
}

export function getJwt() {
  return localStorage.getItem(TokenKey);
}

export function removeToken() {
  localStorage.removeItem(TokenKey);
}

export default {
  login,
  getJwt,
  removeToken,
};
