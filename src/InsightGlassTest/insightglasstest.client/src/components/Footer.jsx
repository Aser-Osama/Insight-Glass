import "bootstrap/dist/css/bootstrap.min.css";
import { Container, Row, Col } from "react-bootstrap";
import { FaFacebook, FaTwitter, FaInstagram } from "react-icons/fa";
// import logo from "imges/Logo.png"; // Adjust the path to your logo image

export default function FooterMain() {
  return (
    <footer
      style={{
        backgroundColor: "rgb(7, 7, 96)",
        color: "white",
        marginTop: "30px",
        paddingTop: "20px",
      }}
    >
      <Container>
        <Row className="justify-content-center">
          <Col className="text-center mb-3 mb-md-0">
            <img
              src="imges/logo.png"
              alt="Logo"
              style={{
                background: "white",
                width: "100px",
                height: "100px",
                borderRadius: "50%",
                marginBottom: "20px",
              }}
            />
          </Col>
        </Row>
        <Row className="justify-content-center">
          <Col className="text-center mb-3 mb-md-0">
            <div>
              <FaFacebook style={{ marginRight: "10px" }} />
              <FaTwitter style={{ marginRight: "10px" }} />
              <FaInstagram />
            </div>
          </Col>
        </Row>
        <Row className="justify-content-center">
          <Col className="text-center  mb-3 mb-md-0">
            <nav aria-label="breadcrumb">
              <ol
                className="breadcrumb"
                style={{
                  backgroundColor: "transparent",
                  marginBottom: 0,
                  display: "flex",
                  justifyContent: "center",
                }}
              >
                <li className="breadcrumb-item">
                  <a href="#" style={{ color: "yellow" }}>
                    Home
                  </a>
                </li>
                <li className="breadcrumb-item">
                  <a href="#" style={{ color: "yellow" }}>
                    About Us
                  </a>
                </li>
                <li className="breadcrumb-item">
                  <a href="#" style={{ color: "yellow" }}>
                    Contact
                  </a>
                </li>
              </ol>
            </nav>
          </Col>
        </Row>
        <Row className="justify-content-center">
          <Col className="text-center">
            <p>All rights resirved © of their respective owners</p>
          </Col>
        </Row>
      </Container>
    </footer>
  );
}
