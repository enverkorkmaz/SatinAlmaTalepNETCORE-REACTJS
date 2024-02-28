import http from "./http";
import { getUser } from "./utils";

export const login = async (email, password) => {
  const response = await http.post("/Auth/Login", { email, password });
  console.log(response.data);
  const accessToken = response.data.accessToken;
  const refreshToken = response.data.refreshToken;
  localStorage.setItem("token", accessToken);
  localStorage.setItem("refreshToken", refreshToken);
};

export const register = async (email,password,role) => {
  await http.post("/Auth/Register",{email,password,role});
}

export const logout = async () => {
  localStorage.removeItem("token");
  localStorage.removeItem("refreshToken");

  const user = getUser();

  try {
    await http.post("/Auth/Revoke", { email: user?.email });
  } catch (error) {
    console.error(error);
  }

  window.location.href = "/";
};

export const getAllProducts = async () => {
  const response = await http.get("/Product/GetAllProducts");
  return response.data;
};

export const createRequest = async (body) => {
  await http.post("/Request/CreateRequest", body);
};

export const getAllRequests = async () => {
  const response = await http.get("/Request/GetAllRequests");
  return response.data;
};

export const getApprovedRequests = async () =>{
  const response = await http.get("/Request/GetApprovedRequests");
  return response.data;
}

export const approveRequest = async (id) => {
  await http.post("/Request/ApproveRequest", { id });
};

export const rejectRequest = async (id) => {
  await http.post("/Request/RejectRequest", { id });
};
