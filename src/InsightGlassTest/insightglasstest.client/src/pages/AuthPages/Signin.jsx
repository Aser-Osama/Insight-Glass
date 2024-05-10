import { Button, Container, Row } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import { Link } from "react-router-dom";

function SignIn() {
  return (
    <div>
      <Row>
        <h1
          style={{
            color: "rgb(7, 7, 96)",
            paddingTop: "50px",
          }}
        >
          Sign In
        </h1>
      </Row>
      <Container
        className="col-3"
        style={{ margin: "flex", paddingTop: "50px", paddingBottom: "50px" }}
      >
        <Form>
          <Form.Group className="mb-3" controlId="formGroupEmail">
            <Form.Control type="email" placeholder="Enter email" />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formGroupPassword">
            <Form.Control type="password" placeholder="Password" />
          </Form.Group>
          <p>
            Don&apos;t have an account? <Link to={"/register"}>Sign Up </Link>
          </p>

          <Button variant="success">Sign in </Button>
        </Form>
      </Container>
    </div>
  );
}

export default SignIn;
