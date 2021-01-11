import { toast } from "react-toastify";

export function success(mesaage, position, time = 5000) {
  toast.success(mesaage, {
    position: position,
    autoClose: time,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
  });
}

export function error(mesaage, position, time = 5000) {
  toast.error(mesaage, {
    position: position,
    autoClose: time,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
  });
}

export const TOP_RIGHT = "top-right";
export const TOP_LEFT = "top-left";
export const BOTTOM_RIGHT = "bottom-right";
export const BOTTOM_LEFT = "bottom-left";

export default {
  success,
  error,
  TOP_LEFT,
  TOP_RIGHT,
  BOTTOM_LEFT,
  BOTTOM_RIGHT,
};
