import axios from "axios";

const apiBaseUrl = process.env.REACT_APP_API_BASE_URL || "http://localhost:5000";

const api = axios.create({
  baseURL: `${apiBaseUrl}/api`,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers = config.headers || {};
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api; 