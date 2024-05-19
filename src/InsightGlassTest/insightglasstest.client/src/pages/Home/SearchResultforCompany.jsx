import { useEffect, useState } from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import { useSearchParams } from "react-router-dom";

function SearchResultForCompany() {
  const [selectedCompany, setSelectedCompany] = useState(null);
  const [searchParams] = useSearchParams();
  useEffect(() => {
    const currentParams = Object.fromEntries([...searchParams]);
    console.log(currentParams); // get new values onchange
  }, [searchParams]);

  const paramObj = Object.fromEntries([...searchParams]);

  const handleCompanySelect = (company) => {
    setSelectedCompany(company);
  };

  return (
    <Container>
      {paramObj.search && <p> You searched for: {paramObj.search}</p>}

      <div className="text-left p-3">
        <Row>
          <Col sm={4}>
            {/* First part */}
            <CompanyInfo
              logo="/imges/c3.png"
              name="Company Name"
              industry="Industry:"
              rate="⭐⭐⭐⭐"
              onSelect={handleCompanySelect}
            />
            <CompanyInfo
              logo="/imges/c1.png"
              name="Company Name"
              industry="Industry:"
              rate="⭐⭐⭐"
              onSelect={handleCompanySelect}
            />

            {/* Repeat this row structure for the other companies */}
          </Col>
          <Col sm={8}>
            {/* Second part */}
            {selectedCompany && (
              <>
                <CompanyInfo
                  logo={selectedCompany.logo}
                  name={selectedCompany.name}
                  industry={selectedCompany.industry}
                  rate={selectedCompany.rate}
                />
                <Row>
                  <Col sm={12}>
                    <p>
                      <strong>Founded:</strong>{" "}
                      <small className="project-description"></small>
                    </p>
                  </Col>
                </Row>
                <Row>
                  <Col sm={12}>
                    <p>
                      <strong>About:</strong>{" "}
                      <small className="project-description"></small>
                    </p>
                  </Col>
                </Row>

                <Row>
                  <Col sm={12}>
                    <Row>
                      <Col sm={12}>
                        <p>
                          <strong>Reviews:</strong>{" "}
                        </p>
                        <Review reviewerName="Name" rating="⭐⭐⭐" review="" />
                        <Review reviewerName="Name" rating="⭐⭐⭐" review="" />

                        <div className="mr-3">
                          <Button variant="success" href="#">
                            Add a Review
                          </Button>
                        </div>
                      </Col>
                    </Row>
                    <hr style={{ marginTop: "25px" }} />
                    <p style={{ marginTop: "25px" }}>
                      <strong>Job opened positions:</strong>{" "}
                    </p>
                    <p>Job Title: </p>
                    <p>Description: </p>
                    <p>Qualification: </p>
                    <Button variant="success" href="#">
                      Apply
                    </Button>
                  </Col>
                </Row>
              </>
            )}
          </Col>
        </Row>
      </div>
    </Container>
  );
}

function CompanyInfo({ logo, name, industry, rate, onSelect }) {
  const handleClick = () => {
    onSelect({ logo, name, industry, rate });
  };

  return (
    <Row onClick={handleClick}>
      <div className="company-info">
        <div className="company-logo">
          <img
            alt=""
            src={logo}
            width="70"
            height="70"
            className="d-inline-block align-top"
          />
        </div>
        <div>
          <p className="mt-0 font-weight-bold">{name}</p>
          <p className="job-title">{industry}</p>
          <p>Rate: {rate}</p>
        </div>
      </div>
    </Row>
  );
}

function Review({ reviewerName, rating, review }) {
  return (
    <Row>
      <Col sm={1}></Col>
      <Col sm={8}>
        <p>
          <strong>Reviewer name:</strong> {reviewerName}
        </p>
        <p>
          <strong>Rating:</strong> {rating}
        </p>
        <p>
          <strong>Review:</strong> {review}
        </p>
        <hr />
      </Col>
    </Row>
  );
}

export default SearchResultForCompany;
