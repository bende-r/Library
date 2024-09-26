import React, { useState, useEffect } from "react";
import PostService from "../services/post.service";
import AuthService from "../services/auth.service";
import { useNavigate, Link } from "react-router-dom";
import handleRefresh from './refresh';

const AdminPage = () => {
  const [books, setBooks] = useState([]);

  const user = AuthService.getCurrentUser();
  const navigate = useNavigate();

  useEffect(() => {
    console.log(user);
    const fetchBooks = async () => {
      await PostService.getAllBooks().then(
        (response) => {
          setBooks(response.data);
        },
        async (error) => {
          if (error.response == null) {
            handleRefresh(user, navigate);
          }
        }
      );
    };
    fetchBooks();
  }, [navigate]);

  const handleDelete = async (bookId) => {
    try {
      await PostService.deleteBook(bookId).then(
        (response) => {
          const updatedBooks = books.filter((book) => book.id !== bookId);
          setBooks(updatedBooks);
          alert("You deleted the book successfully");
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
      console.error("Error deleting book:", error);
    }
  };

  return (
    <div>
      <div className="mt-3">
        <h2>Admin Panel - Books Management</h2>
        <Link to="/addBook" className="btn btn-primary mb-3">Add Book</Link>
        <div className="row row-cols-1 row-cols-md-3 g-4">
          {books.map((book) => (
            <div className="col" key={book.id}>
              <div className="card border-primary">
                <div className="card-header">
                  <Link to={`/bookDetails/${book.id}`}>{book.title}</Link>
                </div>
                <img
                  src={`/pictures/${book.picture || "default.jpg"}`} // Fallback if no image
                  className="card-img-top"
                  alt={book.title}
                  style={{ objectFit: "cover", height: "300px" }}
                />
                <div className="card-body">
                  <p className="card-text">{book.description}</p>
                  <button className="btn btn-danger" onClick={() => handleDelete(book.id)}>Delete</button>
                  <Link to={`/editBook/${book.id}`} className="btn btn-primary">
                    Edit
                  </Link>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default AdminPage;
