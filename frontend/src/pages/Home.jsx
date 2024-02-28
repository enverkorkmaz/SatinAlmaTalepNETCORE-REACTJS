import { logout } from "../utils/api";
import Personel from "./Personel";
import PersonelAmiri from "./PersonelAmiri";
import SatinAlmaMuduru from "./SatinAlmaMuduru";
import Layout from "../components/Layout";
import { getUser } from "../utils/utils";

const Home = () => {
  const user = getUser();

  switch (user?.role) {
    case "personel":
      return (
        <Layout>
          <Personel />
        </Layout>
      );
    case "personelamiri":
      return (
        <Layout>
          <PersonelAmiri />
        </Layout>
      );
    case "satinalmamuduru":
      return (
        <Layout>
          <SatinAlmaMuduru />
        </Layout>
      );
    default:
      logout();
      return null;
  }
};

export default Home;
