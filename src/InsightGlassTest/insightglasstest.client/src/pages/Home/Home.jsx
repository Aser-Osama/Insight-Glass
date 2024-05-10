import { useState } from "react";
import { Carousel, Container, Row, Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

// import LogoutLink from "../../components/LogoutLink.jsx";
// import AuthorizeView, {
//   AuthorizedUser,
// } from "../../components/AuthorizeView.jsx";
//   const [isLoggedIn, setIsLoggedIn] = useState(false);
//   <AuthorizeView
//     showLoadingMsg={false}
//     redirectToLoginPage={false}
//     onIsLoggedIn={setIsLoggedIn}
//   >
//     <span>
//       <LogoutLink>
//         Logout <AuthorizedUser value="email" />
//       </LogoutLink>
//     </span>
//     <p>THIS HIDDENTTTTT</p>
//   </AuthorizeView>
function HomePage() {
  const navigate = useNavigate();
  const [SearchVal, setSearchVal] = useState("");

  return (
    <div>
      <Carousel data-bs-theme="dark">
        <Carousel.Item>
          <img
            className="d-block w-100"
            src="imges/logo.png"
            alt="First slide"
            style={{ height: "60vh", objectFit: "cover" }}
          />
          <Carousel.Caption className="caption-center">
            <div className="caption-content">
              <h5>Humen Empowerment</h5>
              <p>
                Unveil insider insights and salaries with our community-driven
                platform, guiding your career choices with real employee reviews
                and company ratings. Explore exciting job opportunities tailored
                to your skills and aspirations.
              </p>
            </div>
          </Carousel.Caption>
        </Carousel.Item>
        <Carousel.Item>
          <img
            className="d-block w-100"
            src="imges/logo.png"
            alt="Second slide"
            style={{ height: "60vh", objectFit: "cover" }}
          />
          <Carousel.Caption className="caption-center">
            <div className="caption-content">
              <h5>Second slide label</h5>
              <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            </div>
          </Carousel.Caption>
        </Carousel.Item>
      </Carousel>
      <Container className=" --bs-gray-100" fluid>
        <Row
          className="bg-body-tertiary "
          style={{
            color: "rgb(7, 7, 96)",
            paddingTop: "20px",
            paddingBottom: "40px",
          }}
        >
          <h2>Start your search now !</h2>
          <br></br>
          <br></br>
          <br></br>
          <Form
            className="d-flex"
            onSubmit={(e) => e.preventDefault()}
            onKeyDown={(e) =>
              e.key === "Enter"
                ? navigate(`search_company/?search=${SearchVal}`)
                : ""
            }
          >
            <Form.Control
              type="search"
              placeholder="Job Title, Key word, or company"
              className="me-2"
              aria-label="Search"
              value={SearchVal}
              onChange={(e) => setSearchVal(e.target.value)}
            />
            <Button
              variant="outline-success me-2"
              onClick={() => navigate(`search_job/?search=${SearchVal}`)}
            >
              Search for Job
            </Button>
            <Button
              variant="outline-secondary"
              onClick={() => navigate(`search_company/?search=${SearchVal}`)}
            >
              Search for Company
            </Button>
          </Form>
        </Row>
      </Container>
    </div>
  );
}

export default HomePage;
