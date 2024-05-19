import { useState } from "react";
import {
  Container,
  Row,
  Col,
  Button,
  Card,
  ListGroup,
  Image,
} from "react-bootstrap";

function Review_tItem({ review_t }) {
  return (
    <Card className="mb-3">
      <Card.Body>
        <Card.Title>
          {review_t?.name} - {review_t.company}
        </Card.Title>
        <Card.Subtitle>
          Rating:{" "}
          <span className="yellow-stars">{"★".repeat(review_t.rating)}</span>
        </Card.Subtitle>
        <Card.Text>{review_t.comment}</Card.Text>
      </Card.Body>
    </Card>
  );
}

function CompanyProfile() {
  const [review_ts] = useState([
    {
      id: 1,
      name: "John Doe",
      company: "XYZ Corp",
      rating: 4,
      comment: "Good job!",
    },
    {
      id: 2,
      name: "Jane Smith",
      company: "ABC Inc",
      rating: 5,
      comment: "Excellent service.",
    },
  ]);
  const [jobs, setJobs] = useState([
    {
      id: 1,
      title: "Software Developer",
      description: "Develop full-stack software.",
      qualifications: "Experience in React.",
    },
    {
      id: 2,
      title: "Marketing Manager",
      description: "Lead our marketing team.",
      qualifications: "5 years experience.",
    },
  ]);

  const handleRemoveJob = (id) => {
    setJobs(jobs.filter((job) => job.id !== id));
  };

  const averageRating =
    review_ts.reduce((acc, review_t) => acc + review_t.rating, 0) /
    review_ts.length;

  return (
    <Container fluid>
      <Row className="my-4 justify-content-center">
        <Col md={8}>
          <div className="d-flex align-items-center justify-content-between mb-3">
            <Image src="/imges/logo.png" height="150" alt="Company Logo" />
            <div>
              <h2>Company Name</h2>
              <div
                className="yellow-stars"
                style={{ fontSize: "2rem", color: "yellow", display: "block" }}
              >
                {"★".repeat(Math.round(averageRating))}
              </div>
            </div>
            <Button variant="success">Edit general information</Button>
          </div>

          <Card className="mb-3">
            <Card.Header>General Information</Card.Header>
            <ListGroup variant="flush">
              <ListGroup.Item>Founded: 1998</ListGroup.Item>
              <ListGroup.Item>Industry: Technology</ListGroup.Item>
              <ListGroup.Item>About: Leading tech innovator.</ListGroup.Item>
            </ListGroup>
          </Card>

          {review_ts.map((review_t) => (
            <Review_tItem key={review_t.id} review_t={review_t} />
          ))}

          <Card>
            <Card.Header>Job Opened Positions</Card.Header>
            <ListGroup variant="flush">
              {jobs.map((job) => (
                <ListGroup.Item key={job.id}>
                  {job.title} - {job.description} ({job.qualifications})
                  <Button
                    variant="danger"
                    size="sm"
                    onClick={() => handleRemoveJob(job.id)}
                    className="float-right"
                  >
                    Remove
                  </Button>
                </ListGroup.Item>
              ))}
              <ListGroup.Item>
                <Button variant="success">Add</Button>
              </ListGroup.Item>
            </ListGroup>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default CompanyProfile;
