import React, { useState } from 'react';
import { Container, Button, Card, FormCheck } from "react-bootstrap";

// Single review component using Card from React-Bootstrap
function ReviewItem({ review, onChange, isChecked }) {
  return (
    <Card className="mb-3">
      <Card.Body>
        <FormCheck type="checkbox" checked={isChecked} onChange={() => onChange(review.id)}>
          <Card.Title>{review.name} - {review.company}</Card.Title>
          <Card.Subtitle>
            Rating: <span className="yellow-stars">{'★'.repeat(review.rating)}</span>
          </Card.Subtitle>
          <Card.Text>{review.comment}</Card.Text>
        </FormCheck>
      </Card.Body>
    </Card>
  );
}

// Main Admin Page Component
function AdminPage() {
  const [reviews, setReviews] = useState([
    { id: 1, name: "John Doe", company: "XYZ Corp", rating: 4, comment: "Good job!" },
    { id: 2, name: "Jane Smith", company: "ABC Inc", rating: 5, comment: "Excellent service." }
  ]);
  const [selected, setSelected] = useState([]);

  const handleSelect = (id) => {
    const newSelected = selected.includes(id) ? selected.filter(item => item !== id) : [...selected, id];
    setSelected(newSelected);
  };

  const removeSelected = () => {
    setReviews(reviews.filter(review => !selected.includes(review.id)));
    setSelected([]); // Clear selections
  };

  return (
    <>
      <Container className="my-3 text-center">
        <h1 className="my-4">Admin</h1>
        {reviews.map(review => (
          <ReviewItem
            key={review.id}
            review={review}
            onChange={handleSelect}
            isChecked={selected.includes(review.id)}
          />
        ))}
        <Button variant="danger" onClick={removeSelected} className="mt-3">Remove all selected</Button>
      </Container>
    </>
  );
}

export default AdminPage;
