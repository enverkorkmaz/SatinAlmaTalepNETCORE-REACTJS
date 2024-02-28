import { logout } from "../utils/api";
import { getUser } from "../utils/utils";
import { createContext, useState, useContext } from "react";

const LoadingContext = createContext();

export const useSetLoading = () => {
  const context = useContext(LoadingContext);
  if (!context) {
    throw new Error("useSetLoading must be used within a LoadingProvider");
  }

  return context;
};

const Layout = ({ children }) => {
  const [loading, setLoading] = useState(false);
  const user = getUser();

  return (
    <div className="flex flex-col h-screen">
      <div className="flex items-center justify-between px-4 py-4 text-xl bg-gray-800 text-white">
        <div className="">Satın Alma Talep Sistemi</div>
        {user && (
          <div className="flex items-center gap-2">
            <div>
              Kullanıcı: <span>{user.email}</span>
            </div>
            <button className="text-lg bg-red-400 text-black px-2 rounded-lg" onClick={logout}>
              Çıkış
            </button>
          </div>
        )}
      </div>
      <LoadingContext.Provider value={setLoading}>
        <div className="flex-grow">
          {children}
          {loading && (
            <div className="fixed top-0 left-0 w-full h-full bg-gray-100 bg-opacity-50 flex items-center justify-center">
              <div className="bg-white p-4 rounded shadow-lg">
                <div className="animate-spin rounded-full h-20 w-20 border-t-2 border-b-2 border-gray-900"></div>
              </div>
            </div>
          )}
        </div>
      </LoadingContext.Provider>
    </div>
  );
};

export default Layout;
