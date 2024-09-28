import React, { useState, useEffect } from "react";
import AuthService from "../services/auth.service";
import PostService from "../services/post.service";
import { useNavigate, Link } from "react-router-dom";
import handleRefresh from './refresh';

const UserBooks = () => {
  const [userBooks, setUserBooks] = useState([]);
  const [refreshBooks, setRefreshBooks] = useState(false); 
  const user = AuthService.getCurrentUser();
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    PostService.getUsersBooks(user.user.id).then(
      (response) => {
        console.log(response.data);
        setUserBooks(response.data);
        // Добавляем alert для вывода длины массива книг
      //  alert(`You have ${response.data.length} books.`);
      },
      async (error) => {
        if (error.response == null) {
          handleRefresh(user, navigate);
        } else {
          alert(error.response.data['ErrorMessage']);
        }          
      }
    );
  }, [navigate, refreshBooks]);

  const handleReturnBook = (bookId) => async (e) => {
    e.preventDefault();
    try {
      await PostService.returnBook(user.user.id, bookId).then(
        () => {
          setRefreshBooks((prev) => !prev);
        },
        (error) => {
          if (error.response == null) {
            handleRefresh(user, navigate);
          } else {
            setErrorMessage(error.response.data['ErrorMessage']);
          }
        }
      );
    } catch (error) {
      console.error("Error returning book:", error);
    }
  };
  
  return (
    <div>
      <div className="mt-3">
        <h2>My Books</h2>
        <div className="row row-cols-1 row-cols-md-3 g-4">
          {userBooks.map((book) => (
            <div className="col" key={book.id}>
              <div className="card border-primary">
                <div className="card-header">
                  <Link to={`/bookDetails/${book.id}`}>{book.title}</Link>
                </div>
                <img
                  src={`/pictures/${book.imageUrl || "default.jpg"}`}
                  className="card-img-top"
                  alt={book.title}
                  style={{ objectFit: "cover", height: "300px" }}
                />
                <div className="card-body">
                  <h5 className="card-title">Book Details</h5>
                  <p><strong>Title:</strong> {book.title}</p>
                  <p><strong>Author:</strong> {book.authorName}</p>
                  <p><strong>ISBN:</strong> {book.isbn}</p>
                  <p><strong>Genre:</strong> {book.genre}</p>
                  <p><strong>Description:</strong> {book.description}</p>
                  <p><strong>Is Borrowed:</strong> {book.isBorrowed ? 'Yes' : 'No'}</p>
                  
                  <button
                    onClick={handleReturnBook(book.id)}
                    className="btn btn-danger"
                  >
                    Return Book
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default UserBooks;
