import { useEffect, useState } from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import { useSearchParams } from "react-router-dom";
import axios from "axios";

function SearchResultForCompany() {
    const [selectedCompany, setSelectedCompany] = useState(null);
    const [searchResults, setSearchResults] = useState([]);
    const [searchParams] = useSearchParams();

    const fetchParams = async () => {
        const currentParams = Object.fromEntries([...searchParams]);
        if (currentParams.search) {
            try {
                const res = await axios.get(`/api/companies/search?search=${currentParams.search}`);
                if (res.data && res.data.$values) {
                    setSearchResults(res.data.$values); // As the data is inside $values
                } else {
                    setSearchResults([]); // If the data format is not as expected or $values is not present
                }
            } catch (err) {
                console.error("Error fetching company data:", err);
                setSearchResults([]); // Set to empty on error
            }
        }
    }


    useEffect(() => {
        fetchParams();
    }, [searchParams]);

    const handleCompanySelect = (company) => {
        setSelectedCompany(company);
    };

    return (
        <Container>
            {searchParams.get("search") && <p>You searched for: {searchParams.get("search")}</p>}
            <div className="text-left p-3">
                <Row>
                    <Col sm={4}>
                        {searchResults.map((company, index) => (
                            <CompanyInfo
                                key={index}
                                logo={company.logo || '/default-logo.png'}
                                name={company.companyUser?.userName}
                                industry={company.companyIndustry}
                                rate={company.rate}
                                onSelect={() => handleCompanySelect(company)}
                            />
                        ))}
                    </Col>
                    <Col sm={8}>
                        {selectedCompany && (
                            <>
                                <CompanyInfo
                                    logo={selectedCompany.logo}
                                    name={selectedCompany.companyUser?.userName}
                                    industry={selectedCompany.companyIndustry}
                                    rate={selectedCompany.rate}
                                />
                                <Row>
                                    <Col sm={12}>
                                        <p><strong>Founded:</strong> {selectedCompany.founded}</p>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col sm={12}>
                                        <p><strong>About:</strong> {selectedCompany.about}</p>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col sm={12}>
                                        <p><strong>Reviews:</strong></p>
                                        {selectedCompany && selectedCompany.reviews && selectedCompany.reviews.map((review, index) => (
                                            <Review
                                                key={index}
                                                reviewerName={review.reviewerName}
                                                rating={review.rating}
                                                review={review.review}
                                            />
                                        ))}
                                        <div className="mr-3">
                                            <Button variant="success" href="#">Add a Review</Button>
                                        </div>
                                    </Col>
                                    <Col sm={12}>
                                        <p><strong>Job opened positions:</strong></p>
                                        {selectedCompany && selectedCompany.jobs && selectedCompany.jobs.map((job, index) => (
                                            <div key={index}>
                                                <p>Job Title: {job.title}</p>
                                                <p>Description: {job.description}</p>
                                                <p>Qualification: {job.qualification}</p>
                                                <Button variant="success" href="#">Apply</Button>
                                            </div>
                                        ))}
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
                <p><strong>Reviewer name:</strong> {reviewerName}</p>
                <p><strong>Rating:</strong> {rating}</p>
                <p><strong>Review:</strong> {review}</p>
                <hr />
            </Col>
        </Row>
    );
}

export default SearchResultForCompany;
