import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { Link } from "react-router-dom";
import { Row, Col } from "react-bootstrap";
function NavBarMain() {
  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand>
          <Link to={`/`} className="nav-link">
            <img
              alt=""
              src="/imges/logo.png"
              width="30"
              height="30"
              style={{
                background: "white",

                borderRadius: "50%",
              }}
              className="d-inline-block align-top"
            />
            {"  "}
            Insignt Glass
          </Link>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ms-auto">
            <Row className="justify-content-end">
              <Col>
                <Link className="nav-link" to={`signin`}>
                  Sign in
                </Link>
              </Col>
              <Col>
                <Link className="nav-link" to={`register`}>
                  Register
                </Link>
              </Col>
            </Row>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavBarMain;
