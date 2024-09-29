import React, { useState, useEffect } from "react";
import PostService from "../services/post.service";
import AuthService from "../services/auth.service";
import { useNavigate, Link } from "react-router-dom";
import handleRefresh from "./refresh";

const AuthorsPage = () => {
  const [authors, setAuthors] = useState([]);
  const [selectedAuthor, setSelectedAuthor] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const user = AuthService.getCurrentUser();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchAuthors = async () => {
      await PostService.getAllAuthors().then(
        (response) => {
          setAuthors(response.data || []);
        },
        async (error) => {
          if (error.response == null) {
            handleRefresh(user, navigate);
          }
        }
      );
    };
    fetchAuthors();
  }, [navigate]);

  const handleDelete = async () => {
    try {
      await PostService.deleteAuthor(selectedAuthor.id).then(
        (response) => {
          const updatedAuthors = authors.filter(
            (author) => author.id !== selectedAuthor.id
          );
          setAuthors(updatedAuthors);
          closeModal();
        },
        (error) => {
          if (error.response == null) {
            handleRefresh(user, navigate);
          } else {
            alert(error.response.data["ErrorMessage"]);
          }
        }
      );
    } catch (error) {
      console.error("Error deleting author:", error);
    }
  };

  const openModal = (author) => {
    setSelectedAuthor(author);
    setShowModal(true);
  };

  const closeModal = () => {
    setShowModal(false);
    setSelectedAuthor(null);
  };

  return (
    <div>
      <div className="mt-3">
        <h2>Authors Management</h2>
        <Link to="/addAuthor" className="btn btn-primary mb-3">
          Add Author
        </Link>
        <div className="row row-cols-1 row-cols-md-3 g-4">
          {Array.isArray(authors) && authors.length > 0 ? (
            authors.map((author) => (
              <div className="col" key={author.id}>
                <div className="card border-primary">
                  <div className="card-header">
                    <Link to={`/authorDetails/${author.id}`}>
                      {author.firstName} {author.lastName}
                    </Link>
                  </div>
                  <div className="card-body">
                    <p className="card-text">
                      <strong>Country:</strong> {author.country}
                    </p>
                    <p className="card-text">
                      <strong>Date of Birth:</strong>{" "}
                      {new Date(author.dateOfBirth).toLocaleDateString()}
                    </p>
                    <button
                      className="btn btn-danger"
                      onClick={() => openModal(author)}
                    >
                      Delete
                    </button>
                    <Link
                      to={`/editAuthor/${author.id}`}
                      className="btn btn-primary"
                    >
                      Edit
                    </Link>
                  </div>
                </div>
              </div>
            ))
          ) : (
            <p>No authors available.</p>
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
                <button
                  type="button"
                  className="close"
                  onClick={closeModal}
                  aria-label="Close"
                >
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div className="modal-body">
                <p>
                  Are you sure you want to delete the author "
                  <strong>
                    {selectedAuthor?.firstName} {selectedAuthor?.lastName}
                  </strong>
                  "?
                </p>
              </div>
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={closeModal}
                >
                  Cancel
                </button>
                <button
                  type="button"
                  className="btn btn-danger"
                  onClick={handleDelete}
                >
                  Delete
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default AuthorsPage;
