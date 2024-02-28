import * as jose from "jose";

export const getUser = () => {
  const token = localStorage.getItem("token");
  if (!token) {
    return null;
  }

  const decoded = jose.decodeJwt(token);
  console.log(decoded);
  return {
    email: decoded.email,
    role: decoded.role,
  };
};
