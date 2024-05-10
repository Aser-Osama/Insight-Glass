import { Container, Row, Col, Button } from "react-bootstrap";
export default function Homefirstview() {
  return (
    <Container style={{ paddingTop: "40px", paddingBottom: "40px" }}>
      <Row className="justify-content-center">
        <Col className="text-center mb-3 mb-md-0">
          <img
            src="imges/Logo.png"
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
        <p>Creat an acount or sign in.</p>
      </Row>
      <Row className="justify-content-center">
        <p>
          By Continuing you are agree to our <a href="#">Terms of use </a> and{" "}
          <a href="#">Privacy Policy</a>
        </p>
      </Row>
      <br></br>
      <Button variant="success">Sign in </Button>
    </Container>
  );
}
