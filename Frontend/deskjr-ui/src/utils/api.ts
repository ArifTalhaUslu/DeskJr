import axios from "axios";

const token = localStorage.getItem("token");

const api = axios.create({
  baseURL: "https://localhost:7187",
  headers: {
    Authorization:`Bearer ${localStorage.getItem("token")}`
  },
});

export default api;
