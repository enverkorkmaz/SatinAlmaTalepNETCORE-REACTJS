import { useEffect } from "react";
import { register } from "../utils/api";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const navigate = useNavigate();



  const onSubmit = async (event) => {
    event.preventDefault();
    const email = event.target.email.value;
    const password = event.target.password.value;
    const role = event.target.role.value;

    try {
      await register(email, password,role);
    //   alert("KAYIT BAŞARILI! GİRİŞ YAPINIZ");
      navigate("/home");
    } catch (error) {
      alert(error);
    }
  };


  return (
    <div className="flex items-center justify-center h-screen bg-gray-100">
      <div className="bg-white p-8 rounded shadow-md">
        <h2 className="text-2xl font-bold mb-4">Register</h2>
        <form onSubmit={onSubmit}>
          <div className="mb-4">
            <label htmlFor="email" className="block text-gray-700 font-bold mb-2">
              Email
            </label>
            <input
              type="email"
              id="email"
              className="w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500"
              placeholder="Enter your email"
            />
          </div>
          <div className="mb-4">
            <label htmlFor="password" className="block text-gray-700 font-bold mb-2">
              Password
            </label>
            <input
              type="password"
              id="password"
              className="w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500"
              placeholder="Enter your password"
            />
          </div>
          <div className="mb-4">
          <label htmlFor="role" className="block text-gray-700 font-bold mb-2">
            Role
          </label>
            <select id="role">
            <option key="personel" value="personel">
                Personel
              </option>
              <option key="personelamiri" value="personelamiri">
                Personel Amiri
              </option>
              <option key="satinalmamuduru" value="satinalmamuduru">
                Satin Alma Muduru
              </option>
            </select>
          </div>
          <button
            type="submit"
            className="w-full bg-indigo-500 text-white py-2 px-4 rounded hover:bg-indigo-600 focus:outline-none"
          >
            Register
          </button>
        </form>
      </div>
    </div>
  );
};

export default Register;
