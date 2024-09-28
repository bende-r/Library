import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import PostService from "../services/post.service";
import AuthService from "../services/auth.service";
import { useNavigate } from "react-router-dom";
import handleRefresh from "./refresh";

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

  const handleBorrow = async () => {
    try {
      const currentDate = new Date(); // Дата взятия книги
      const returnDate = new Date();
      returnDate.setDate(currentDate.getDate() + 30); // Возврат через 30 дней

      const borrowRequestBody = {
        userId: user.user.id, // Идентификатор пользователя
        bookId: id, // Идентификатор книги
        borrowDate: currentDate.toISOString(), // Дата взятия книги
        returnDate: returnDate.toISOString(), // Дата возврата книги
      };

      const response = await PostService.borrowBook(
        borrowRequestBody.userId,
        borrowRequestBody.bookId,
        borrowRequestBody.borrowDate,
        borrowRequestBody.returnDate
      );

      console.log("Book borrowed successfully:", response);
      alert("You have successfully borrowed the book");
      navigate("/myBooks");
    } catch (error) {
      console.error("Error borrowing the book:", error);
      alert("An error occurred while borrowing the book.");
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      {bookDetails && (
        <div className="card border-primary">
          <div className="card-header">{bookDetails.title} </div>
          <div className="card-body">
            <img
              src={`/pictures/${bookDetails.imageUrl}`}
              className="card-img-top"
              alt={bookDetails.title}
              style={{ objectFit: "cover", height: "300px", width: "300px" }}
            />
            <p className="card-text">{bookDetails.description}</p>
            <p className="card-text">
              <small className="text-muted">
                Author: {bookDetails.authorName}
              </small>
            </p>
            <p className="card-text">
              <small className="text-muted">Genre: {bookDetails.genre}</small>
            </p>
            <p className="card-text">
              <small className="text-muted">ISBN: {bookDetails.isbn}</small>
            </p>
            <p className="card-text">
              <small className="text-muted">
                Available: {bookDetails.isBorrowed ? "No" : "Yes"}
              </small>
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
