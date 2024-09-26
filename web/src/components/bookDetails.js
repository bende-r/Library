import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import PostService from "../services/post.service";
import AuthService from "../services/auth.service";
import { useNavigate } from "react-router-dom";
import handleRefresh from './refresh';

const BookDetails = () => {
  const { id } = useParams();
  const [bookDetails, setBookDetails] = useState(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const user = AuthService.getCurrentUser();

  useEffect(() => {
    const fetchBookDetails = async () => {
      try {
        await PostService.getBookById(id).then(
          (response) => {
            setBookDetails(response.data);
            setLoading(false);
          },
          (error) => {
            if (error.response == null) {
              handleRefresh(user, navigate);
            }
          }
        );
      } catch (error) {
        setLoading(false);
        console.error("Error fetching book details:", error);
      }
    };

    fetchBookDetails();
  }, [id]);

  const handleBorrow = async (e) => {
    e.preventDefault();
    try {
      await PostService.borrowBook(user.user.id, id).then(
        () => {
          alert('You borrowed the book successfully!');
          window.location.reload();
        },
        (error) => {
          if (error.response == null) {
            handleRefresh(user, navigate);
          } else {
            alert(error.response.data['ErrorMessage']);
          }
        }
      );
    } catch (error) {
      console.error("Error borrowing the book:", error);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      {bookDetails && (
        <div className="card border-primary">
          <div className="card-header">
            {bookDetails.title}{" "}
          </div>
          <div className="card-body">
            <img
              src={`/pictures/${bookDetails.picture}`}
              className="card-img-top"
              alt={bookDetails.title}
              style={{ objectFit: "cover", height: "300px", width: "300px" }}
            />
            <p className="card-text">{bookDetails.description}</p>
            <p className="card-text">
              <small className="text-muted">Author: {bookDetails.authorName}</small>
            </p>
            <p className="card-text">
              <small className="text-muted">Genre: {bookDetails.genre}</small>
            </p>
            <p className="card-text">
              <small className="text-muted">ISBN: {bookDetails.isbn}</small>
            </p>
            <p className="card-text">
              <small className="text-muted">Available: {bookDetails.isBorrowed ? "No" : "Yes"}</small>
            </p>
            {user && !bookDetails.isBorrowed && (
              <button onClick={handleBorrow} className="btn btn-success">
                Borrow
              </button>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default BookDetails;