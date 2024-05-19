import { Button, Container, Row } from "react-bootstrap";

function UserProfile() {
  return (
    <div>
      <Container>
        <Row className="user-info align-items-center">
          <div className="user-details p-5">
            <div className="user-logo">
              <img
                alt=""
                src="/imges/c3.png"
                width="70"
                height="70"
                className="d-inline-block align-top"
              />
            </div>
            <div className="user-name">
              <p>User name</p>
            </div>
            <div>
              <Button
                variant="success"
                className="profile-button"
                style={{ marginRight: "auto" }}
              >
                Edit Profile
              </Button>
            </div>
          </div>
          <div>
            <Button variant="success" className="application-button-row ">
              Job Application
            </Button>
          </div>
        </Row>
      </Container>
      <Container>
        <h2 className="user-ginfo">
          <strong>General Information</strong>
        </h2>
        <Row className="user-ginfo">
          <p>
            <strong>Age: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Job title: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Education level: </strong>
          </p>
        </Row>
        <div></div>
        <h2 className="user-ginfo">
          <strong>Experience</strong>
        </h2>
        <Row className="user-ginfo">
          <p>
            <strong>Position: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Company: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Start date: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>End date: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Location: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Location Type: </strong>
          </p>
        </Row>

        <h2 className="user-ginfo">
          <strong>Education</strong>
        </h2>
        <Row className="user-ginfo">
          <p>
            <strong>Degree: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Institution: </strong>
          </p>
        </Row>
        <Row className="user-ginfo">
          <p>
            <strong>Graduation year: </strong>
          </p>
        </Row>
      </Container>
    </div>
  );
}

export default UserProfile;
