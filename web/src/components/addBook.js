import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import PostService from "../services/post.service";
import handleRefresh from './refresh';
import AuthService from "../services/auth.service";

const EditBook = () => {
  const user = AuthService.getCurrentUser();
  const [errorMessage, setErrorMessage] = useState('');
  const [formData, setFormData] = useState({
    title: "",
    author: "",
    genre: "",
    isbn: "",
    description: "",
    isBorrowed: false,
  });

  const navigate = useNavigate();

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
      await PostService.addBook(formData).then((response) => {
        navigate("/books");
        window.location.reload();
      },
      (error) => {
        if (error.response == null) {
          handleRefresh(user, navigate);
        } else {
          setErrorMessage(error.response.data['ErrorMessage']);
        }
      });
    } catch (error) {
      console.error("Error adding book:", error);
    }
  };

  return (
    <div className="container mt-4">
      <h2>Add Book</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="title" className="form-label">Title:</label>
          <input type="text" className="form-control" id="title" name="title" value={formData.title} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="author" className="form-label">Author:</label>
          <input type="text" className="form-control" id="author" name="author" value={formData.author} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="genre" className="form-label">Genre:</label>
          <input type="text" className="form-control" id="genre" name="genre" value={formData.genre} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="isbn" className="form-label">ISBN:</label>
          <input type="text" className="form-control" id="isbn" name="isbn" value={formData.isbn} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="description" className="form-label">Description:</label>
          <textarea className="form-control" id="description" name="description" value={formData.description} onChange={handleInputChange} required />
        </div>
        <button type="submit" className="btn btn-primary">Add Book</button>
      </form>
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
};

export default EditBook;
