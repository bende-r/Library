import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import PostService from "../services/post.service";
import handleRefresh from "./refresh";
import AuthService from "../services/auth.service";
import { genreOptions } from "./optionsData";

const EditBook = () => {
  const user = AuthService.getCurrentUser();
  const [errorMessage, setErrorMessage] = useState("");
  const [formData, setFormData] = useState({
    isbn: "",
    title: "",
    genre: "",
    description: "",
    authorId: "",
    isBorrowed: false,
  });
  const [authors, setAuthors] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchAuthorsAndGenres = async () => {
      try {
        const authorResponse = await PostService.getAllAuthors();
        setAuthors(authorResponse.data);
      } catch (error) {
        console.error("Error fetching authors or genres:", error);
      }
    };
    fetchAuthorsAndGenres();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await PostService.addBook(formData).then(
        (response) => {
          navigate("/books");
          window.location.reload();
        },
        (error) => {
          if (error.response == null) {
            handleRefresh(user, navigate);
          } else {
            setErrorMessage(error.response.data["ErrorMessage"]);
          }
        }
      );
    } catch (error) {
      console.error("Error adding book:", error);
    }
  };

  return (
    <div className="container mt-4">
      <h2>Add Book</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="title" className="form-label">
            Title:
          </label>
          <input
            type="text"
            className="form-control"
            id="title"
            name="title"
            value={formData.title}
            onChange={handleInputChange}
            required
          />
        </div>

        {/* Выбор автора */}
        <div className="mb-3">
          <label htmlFor="authorId" className="form-label">
            Author:
          </label>
          <select
            className="form-control"
            id="authorId"
            name="authorId"
            value={formData.authorId}
            onChange={handleInputChange}
            required
          >
            <option value="">Select Author</option>
            {authors.map((author) => (
              <option key={author.id} value={author.id}>
                {author.firstName} {author.lastName} {author.country}
              </option>
            ))}
          </select>
        </div>

        {/* Выбор жанра */}
        <div className="mb-3">
          <label htmlFor="genre" className="form-label">
            Genre:
          </label>
          <select
            className="form-control"
            id="genre"
            name="genre"
            value={formData.genre}
            onChange={handleInputChange}
            required
          >
            <option value="">Select Genre</option>
            {genreOptions.map((genre) => (
              <option key={genre.id} value={genre.value}>
                {genre.label}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label htmlFor="isbn" className="form-label">
            ISBN:
          </label>
          <input
            type="text"
            className="form-control"
            id="isbn"
            name="isbn"
            value={formData.isbn}
            onChange={handleInputChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="description" className="form-label">
            Description:
          </label>
          <textarea
            className="form-control"
            id="description"
            name="description"
            value={formData.description}
            onChange={handleInputChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary">
          Add Book
        </button>
      </form>
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
};

export default EditBook;
