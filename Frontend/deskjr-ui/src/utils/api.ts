import axios from "axios";

const token = localStorage.getItem("token");

const api = axios.create({
  baseURL: "https://localhost:7187",
  headers: {
    Authorization: token ? `Bearer ${token}` : '',
  },
});

export default api;
