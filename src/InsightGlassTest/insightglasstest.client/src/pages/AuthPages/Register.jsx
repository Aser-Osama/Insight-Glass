import { useState } from "react";
import { Button, Container, Row } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import { Link } from "react-router-dom";
import axios from "axios";

export default function SignUpPage() {
  // state variables for email and passwords
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  // state variable for error messages
  const [error, setError] = useState("");

  // handle change events for input fields
  const handleChange = async (e) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
    if (name === "password") setPassword(value);
    if (name === "confirmPassword") setConfirmPassword(value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    // validate email and passwords
    if (!email || !password || !confirmPassword) {
      setError("Please fill in all fields.");
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      setError("Please enter a valid email address.");
    } else if (password !== confirmPassword) {
      setError("Passwords do not match.");
    } else {
      // clear error message
      setError("");
      try {
        // post data to the /register api
        const response = await axios.post("/register", {
          email: email,
          password: password,
        });
        console.log(response.data);
        if (response.data.ok) {
          setError("Successful register.");
        } else {
          setError("An error occurred during registration.");
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
          Sign Up
        </h1>
      </Row>
      <Container
        className="col-3"
        style={{ margin: "flex", paddingTop: "50px", paddingBottom: "50px" }}
      >
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3">
            <Form.Control
              placeholder="Enter email"
              type="email"
              id="email"
              name="email"
              value={email}
              onChange={handleChange}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              placeholder="Password"
              type="password"
              id="password"
              name="password"
              value={password}
              onChange={handleChange}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              placeholder="Confirm Password"
              type="password"
              id="confirmPassword"
              name="confirmPassword"
              value={confirmPassword}
              onChange={handleChange}
            />
          </Form.Group>
          <p>
            Already have an account? <Link to={"/signin"}>Sign in </Link>
          </p>

          <Button type="submit" variant="success">
            Sign Up{" "}
          </Button>
        </Form>

        {error && <p className="error">{error}</p>}
      </Container>
    </div>
  );
}
