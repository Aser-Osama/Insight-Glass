import { useEffect, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { useSearchParams } from "react-router-dom";

function SearchResultForJob() {
  const [searchParams] = useSearchParams();
  useEffect(() => {
    const currentParams = Object.fromEntries([...searchParams]);
    console.log(currentParams); // get new values onchange
  }, [searchParams]);

  const paramObj = Object.fromEntries([...searchParams]);

  const companyInfo = [
    {
      logo: "/imges/c1.png",
      name: "Company 1",
      title: "Job Title 1",
      location: "Location 1",
    },
    {
      logo: "/imges/logo.png",
      name: "Company 2",
      title: "Job Title 2",
      location: "Location 2",
    },
    {
      logo: "/imges/c5.png",
      name: "Company 3",
      title: "Job Title 3",
      location: "Location 3",
    },
  ];

  const projectDescription =
    "The intern will assist in creating business materials such as company profiles and pitch decks and will be involved in fundraising activities including investor outreach and meeting scheduling.";

  const qualification =
    "Experience Needed: 0 To 1 Year Career Level: Student (Undergrad / Postgrad) Education Level: Not Specified";

  const [selectedCompany, setSelectedCompany] = useState(null);
  const [hoverSelectedCompany, sethoverSelectedCompany] = useState(null);

  const handleCompanySelect = (index) => {
    setSelectedCompany((i) => (index === i ? null : index));
  };

  return (
    <Container>
      {paramObj.search && <p> You searched for: {paramObj.search}</p>}
      <div className="text-left p-3">
        <Row>
          <Col sm={2}>
            {companyInfo.map((company, index) => (
              <Row
                key={index}
                onClick={() => handleCompanySelect(index)}
                className={`${selectedCompany === index ? "selected" : ""}`}
                style={{
                  backgroundColor:
                    hoverSelectedCompany === index ? "lightgray" : "",
                }}
                onMouseEnter={() => sethoverSelectedCompany(index)}
                onMouseLeave={() => sethoverSelectedCompany(null)}
              >
                <div className="company-info text-center">
                  <div className="company-logo text-center">
                    <img
                      alt=""
                      src={company.logo}
                      width="70"
                      height="70"
                      className="d-inline-block align-top"
                    />
                  </div>
                  <div>
                    <p className="mt-0">{company.name}</p>
                    <p className="job-title" style={{ fontWeight: "bold" }}>
                      {company.title}
                    </p>
                    <p>{company.location}</p>
                  </div>
                </div>
              </Row>
            ))}
          </Col>
          <Col sm={10}>
            {/*main view */}
            <Row>
              <Col sm={12}>
                {selectedCompany !== null && (
                  <div className="company-info ">
                    <div className="company-logo">
                      <img
                        alt=""
                        src={companyInfo[selectedCompany].logo}
                        width="70"
                        height="70"
                        className="d-inline-block align-top"
                      />
                    </div>
                    <div>
                      <p className="mt-0">
                        {companyInfo[selectedCompany].name}
                      </p>
                      <p className="job-title">
                        {companyInfo[selectedCompany].title}
                      </p>
                      <p>{companyInfo[selectedCompany].location}</p>
                    </div>
                    <Row>
                      <Col sm={12}>
                        <p>
                          <strong>Project Description:</strong>{" "}
                          <small className="project-description">
                            {projectDescription}
                          </small>
                        </p>
                      </Col>
                    </Row>
                    <Row>
                      <Col sm={12}>
                        <p>
                          <strong>Qualification:</strong>{" "}
                          <small className="project-description">
                            {qualification}
                          </small>
                        </p>
                      </Col>
                    </Row>
                  </div>
                )}
              </Col>
            </Row>
          </Col>
        </Row>
      </div>
    </Container>
  );
}

export default SearchResultForJob;
