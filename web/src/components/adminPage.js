import React, { useState, useEffect } from "react";
import PostService from "../services/post.service";
import AuthService from "../services/auth.service";
import { useNavigate, Link } from "react-router-dom";
import handleRefresh from './refresh';

const AdminPage = () => {
  const [books, setBooks] = useState([]);
  const [selectedBook, setSelectedBook] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const user = AuthService.getCurrentUser();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchBooks = async () => {
      await PostService.getAllBooks().then(
        (response) => {
          setBooks(response.data.items || []);
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

  const handleDelete = async () => {
    try {
      await PostService.deleteBook(selectedBook.id).then(
        (response) => {
          const updatedBooks = books.filter((book) => book.id !== selectedBook.id);
          setBooks(updatedBooks);
        //   alert("You deleted the book successfully");
          closeModal();
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

  const openModal = (book) => {
    setSelectedBook(book);
    setShowModal(true);
  };

  const closeModal = () => {
    setShowModal(false);
    setSelectedBook(null);
  };

  return (
    <div>
      <div className="mt-3">
        <h2>Admin Panel - Books Management</h2>
        <Link to="/addBook" className="btn btn-primary mb-3">Add Book</Link>
        <div className="row row-cols-1 row-cols-md-3 g-4">
          {Array.isArray(books) && books.length > 0 ? (
            books.map((book) => (
              <div className="col" key={book.id}>
                <div className="card border-primary">
                  <div className="card-header">
                    <Link to={`/bookDetails/${book.id}`}>{book.title}</Link>
                  </div>
                  <img
                    src={`/pictures/${book.picture || "default.jpg"}`}
                    className="card-img-top"
                    alt={book.title}
                    style={{ objectFit: "cover", height: "300px" }}
                  />
                  <div className="card-body">
                    <p className="card-text">{book.description}</p>
                    <button className="btn btn-danger" onClick={() => openModal(book)}>Delete</button>
                    <Link to={`/editBook/${book.id}`} className="btn btn-primary">
                      Edit
                    </Link>
                  </div>
                </div>
              </div>
            ))
          ) : (
            <p>No books available.</p>
          )}
        </div>
      </div>

      {/* Модальное окно для подтверждения удаления */}
      {showModal && (
        <div className="modal-overlay">
          <div className="modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Confirm Deletion</h5>
                <button type="button" className="close" onClick={closeModal} aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div className="modal-body">
                <p>Are you sure you want to delete the book titled "<strong>{selectedBook?.title}</strong>"?</p>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary" onClick={closeModal}>Cancel</button>
                <button type="button" className="btn btn-danger" onClick={handleDelete}>Delete</button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default AdminPage;
