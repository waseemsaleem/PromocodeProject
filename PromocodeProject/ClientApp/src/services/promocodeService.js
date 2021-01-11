import http from './httpService';
import authService from './authService';
import {baseUrl, promoControllerUrl, getAllPromoUrl, togglePromoActivationUrl} from '../api.json';

http.setJwt(authService.getJwt());
const controllerUrl = baseUrl + promoControllerUrl;

async function getAllPromoService(title) {
    const url = title ? `${controllerUrl}${getAllPromoUrl}?title=${title}` : (controllerUrl + getAllPromoUrl);
    return await http.get(url);
}

async function togglePromoActivation(promo) {
    const url = controllerUrl + togglePromoActivationUrl;
    return await http.post(url, promo);
}

export default {
    getAllPromoService,
    togglePromoActivation
}