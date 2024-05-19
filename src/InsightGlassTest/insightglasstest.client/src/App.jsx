import "bootstrap/dist/css/bootstrap.min.css";
import { Outlet } from "react-router-dom";
import NavBarMain from "./components/Nav";
import FooterMain from "./components/Footer";
import { Container } from "react-bootstrap";
function App() {
  return (
    <>
      <NavBarMain />
      <Container className="container-xl p-3" style={{ minHeight: "80vh" }}>
        <Outlet />
      </Container>
      <FooterMain />
    </>
  );
}

export default App;
