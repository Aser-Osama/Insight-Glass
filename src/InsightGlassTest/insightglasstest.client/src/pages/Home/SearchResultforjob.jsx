import { useEffect, useState } from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import { useSearchParams } from "react-router-dom";
import axios from "axios";

function SearchResultForJob() {
    const [selectedJob, setSelectedJob] = useState(null);
    const [searchResults, setSearchResults] = useState([]);
    const [searchParams] = useSearchParams();

    const fetchParams = async () => {
        const currentParams = Object.fromEntries([...searchParams]);
        if (currentParams.search) {
            try {
                const res = await axios.get(`/api/jobs/search?search=${currentParams.search}`);
                setSearchResults(res.data.$values);  // Update to match the structure of your data
            } catch (err) {
                console.log("Error fetching search results:", err);
            }
        }
    };

    useEffect(() => {
        fetchParams();
    }, [searchParams]);

    const handleJobSelect = (job) => {
        setSelectedJob(job);
    };

    return (
        <Container>
            {searchParams.get("search") && <p>You searched for: {searchParams.get("search")}</p>}
            <div className="text-left p-3">
                <Row>
                    <Col sm={4}>
                        {searchResults.map((job, index) => (
                            <JobInfo
                                key={index}
                                logo={job.logo || '/default-logo.png'}
                                title={job.jobTitle}
                                location={job.jobLocation}
                                company={job.companyUser?.userName}
                                onSelect={() => handleJobSelect(job)}
                            />
                        ))}
                    </Col>
                    <Col sm={8}>
                        {selectedJob && (
                            <>
                                <JobInfo
                                    logo={selectedJob.logo}
                                    title={selectedJob.jobTitle}                                />
                                <Row>
                                    <Col sm={12}>
                                        <p><strong>Job Description:</strong> {selectedJob.jobDetails}</p>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col sm={12}>
                                        <p><strong>Salary:</strong> {selectedJob.jobSalary}</p>
                                    </Col>
                                </Row>
                                <div className="mr-3">
                                    <Button variant="success" href="#">Apply</Button>
                                </div>
                            </>
                        )}
                    </Col>
                </Row>
            </div>
        </Container>
    );
}

function JobInfo({ logo, title, location, company, onSelect }) {
    return (
        <Row onClick={onSelect}>
            <div className="job-info">
                <div className="job-logo">
                    <img
                        alt=""
                        src={logo}
                        width="70"
                        height="70"
                        className="d-inline-block align-top"
                    />
                </div>
                <div>
                    <p className="mt-0 font-weight-bold">{title}</p>
                    <p className="job-location">{location}</p>
                    <p>{company}</p>
                </div>
            </div>
        </Row>
    );
}

export default SearchResultForJob;
