import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import PostService from "../services/post.service";
import handleRefresh from "./refresh";
import AuthService from "../services/auth.service";

const AddAuthor = () => {
  const user = AuthService.getCurrentUser();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    dateOfBirth: "",
    country: "",
  });
  const [errorMessage, setErrorMessage] = useState("");

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
      await PostService.addAuthor(formData).then(
        (response) => {
          navigate("/authors");
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
      console.error("Error adding author:", error);
    }
  };

  return (
    <div className="container mt-4">
      <h2>Add Author</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="firstName" className="form-label">
            First Name:
          </label>
          <input
            type="text"
            className="form-control"
            id="firstName"
            name="firstName"
            value={formData.firstName}
            onChange={handleInputChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="lastName" className="form-label">
            Last Name:
          </label>
          <input
            type="text"
            className="form-control"
            id="lastName"
            name="lastName"
            value={formData.lastName}
            onChange={handleInputChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="dateOfBirth" className="form-label">
            Date of Birth:
          </label>
          <input
            type="date"
            className="form-control"
            id="dateOfBirth"
            name="dateOfBirth"
            value={formData.dateOfBirth}
            onChange={handleInputChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="country" className="form-label">
            Country:
          </label>
          <input
            type="text"
            className="form-control"
            id="country"
            name="country"
            value={formData.country}
            onChange={handleInputChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary">
          Add Author
        </button>
      </form>
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
};

export default AddAuthor;
