import axios from "axios";
import router from '/src/router'

const api = axios.create({
 baseURL: "http://localhost:5000/api",
 headers: {
    'Content-Type': 'application/json',
    "Access-Control-Allow-Origin": "*",
  }
});

api.interceptors.response.use(function (response) {

  return response.data;
}, function (error) {
  if(error.response.status == 401){
    router.push("/login")
  }
  return Promise.reject(error.response.data);
});

api.interceptors.request.use(function (config) {
  var token = localStorage.getItem("token")
  config.headers.Authorization = "Bearer " + token;
  return config;
}, function (error) {
  return Promise.reject(error.response.data);
});

export default api