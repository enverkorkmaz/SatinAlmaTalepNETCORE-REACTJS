import axios from "axios";

const http = axios.create({
  baseURL: "/api",
});

http.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

http.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (
      error?.response?.status === 401 &&
      error?.config?.url !== "/Auth/RefreshToken" &&
      error?.config?.url !== "/Auth/Login" &&
      error?.config?.url !== "/Auth/Revoke"
    ) {
      const localRefreshToken = localStorage.getItem("refreshToken");
      if (!localRefreshToken) {
        window.location.href = "/";
        return Promise.reject(error);
      }
      const response = await http.post("/Auth/RefreshToken", { refreshToken: localRefreshToken });
      const { token, refreshToken } = response.data;
      localStorage.setItem("token", token);
      localStorage.setItem("refreshToken", refreshToken);
      return http.request(error.config);
    }

    if (error?.response?.status === 401) {
      window.location.href = "/";
    }

    return Promise.reject(error);
  },
);

export default http;
