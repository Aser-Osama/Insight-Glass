import { useState } from "react";
import { Button, Container, Row } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import { Link } from "react-router-dom";
import axios from "axios";

function SignIn() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberme, setRememberme] = useState(false);
  // state variable for error messages
  const [error, setError] = useState("");

  // handle change events for input fields
  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
    if (name === "password") setPassword(value);
    if (name === "rememberme") setRememberme(e.target.checked);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    // validate email and passwords
    if (!email || !password) {
      setError("Please fill in all fields.");
    } else {
      // clear error message
      setError("");
      // post data to the /register api

      var loginurl = "";
      if (rememberme == true) loginurl = "/login?useCookies=true";
      else loginurl = "/login?useSessionCookies=true";

      try {
        const response = await axios.post(loginurl, {
          email: email,
          password: password,
        });
        // handle success from the server
        console.log(response);
        if (response.status === 200) {
          setError("Successful Login.");
          window.location.href = "/";
        } else {
          setError("Error Logging In.");
        }
      } catch (error) {
        // handle network error
        if (error.response?.data?.errors) {
          setError(
            Object.values(error.response.data.errors).join("\n") ||
              "An error occurred."
          );
        } else {
          setError(error.response?.data?.body || "An error occurred.");
        }
      }
    }
  };

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
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Control
              type="email"
              placeholder="Enter email"
              id="email"
              name="email"
              value={email}
              onChange={handleChange}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              type="password"
              id="password"
              name="password"
              value={password}
              onChange={handleChange}
              placeholder="Password"
            />
          </Form.Group>
          <Form.Group>
            <Form.Check
              type="checkbox"
              id="rememberme"
              name="rememberme"
              checked={rememberme}
              onChange={handleChange}
              label="Remember Me"
            />
          </Form.Group>
          <p>
            Don&apos;t have an account? <Link to={"/register"}>Sign Up </Link>
          </p>

          <Button variant="success" type="submit">
            Sign in
          </Button>
        </Form>
        {error && <p className="error">{error}</p>}
      </Container>
    </div>
  );
}

export default SignIn;
