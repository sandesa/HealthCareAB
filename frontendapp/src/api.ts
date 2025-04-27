import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5249/",
    withCredentials: true,
});

export default api;