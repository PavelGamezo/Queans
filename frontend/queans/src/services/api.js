import axios from "axios";

export const API_URL = "https://localhost:7008/api";

const api = axios.create({
    baseURL: "https://localhost:7008/api",
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token && token !== "undefined" && token !== "null"){
        config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`
    }
    else {
      console.log("Токена нет — запрос идет без Authorization");
    }
    return config;
    },
    (error) => {
    return Promise.reject(error); // ошибка запроса
});

export default api;