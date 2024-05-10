import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { Link, NavLink } from "react-router-dom";
import { Row, Col } from "react-bootstrap";

import LogoutLink from "../components/LogoutLink.jsx";
import AuthorizeView, { AuthorizedUser } from "../components/AuthorizeView.jsx";
import { useState } from "react";

function NavBarMain() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
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
              <AuthorizeView
                showLoadingMsg={false}
                redirectToLoginPage={false}
                onIsLoggedIn={setIsLoggedIn}
              >
                <Col>
                  <NavLink className="nav-link" to={`userprofile`}>
                    Company Profile
                  </NavLink>
                </Col>
                <Col>
                  <NavLink className="nav-link" to={`companyprofile`}>
                    User Profile
                  </NavLink>
                </Col>
                <Col>
                  <NavLink className="nav-link" to={`adminprofile`}>
                    User Profile
                  </NavLink>
                </Col>
                <Col>
                  <LogoutLink>
                    <p className="nav-link">
                      Logout <AuthorizedUser value="email" />
                    </p>
                  </LogoutLink>
                </Col>
              </AuthorizeView>

              {!isLoggedIn && (
                <>
                  <Col>
                    <NavLink className="nav-link" to={`signin`}>
                      Sign in
                    </NavLink>
                  </Col>
                  <Col>
                    <NavLink className="nav-link" to={`register`}>
                      Register
                    </NavLink>
                  </Col>
                </>
              )}
            </Row>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavBarMain;
