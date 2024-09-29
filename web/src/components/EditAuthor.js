import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import PostService from "../services/post.service";
import handleRefresh from "./refresh";
import AuthService from "../services/auth.service";

const EditAuthor = () => {
  const { id } = useParams();
  const user = AuthService.getCurrentUser();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    id: "",
    firstName: "",
    lastName: "",
    dateOfBirth: "",
    country: "",
  });
  const [loading, setLoading] = useState(true);
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    const fetchAuthor = async () => {
      try {
        const response = await PostService.getAuthorById(id);
        setFormData({
          id: response.data.id,
          firstName: response.data.firstName,
          lastName: response.data.lastName,
          dateOfBirth: response.data.dateOfBirth.split("T")[0], // форматирование для date input
          country: response.data.country,
        });
        setLoading(false);
      } catch (error) {
        console.error("Error fetching author:", error);
        setLoading(false);
      }
    };
    fetchAuthor();
  }, [id]);

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
      await PostService.updateAuthor(formData).then(
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
      console.error("Error updating author:", error);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="container mt-4">
      <h2>Edit Author</h2>
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
          Save Changes
        </button>
      </form>
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
};

export default EditAuthor;
