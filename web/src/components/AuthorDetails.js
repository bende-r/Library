import React, { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import PostService from "../services/post.service";

const AuthorDetails = () => {
  const { id } = useParams();
  const [author, setAuthor] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAuthor = async () => {
      try {
        const response = await PostService.getAuthorById(id);
        setAuthor(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching author:", error);
        setLoading(false);
      }
    };
    fetchAuthor();
  }, [id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!author) {
    return <div>Author not found.</div>;
  }

  return (
    <div className="container mt-4">
      <h2>Author Details</h2>
      <p>
        <strong>First Name:</strong> {author.firstName}
      </p>
      <p>
        <strong>Last Name:</strong> {author.lastName}
      </p>
      <p>
        <strong>Date of Birth:</strong>{" "}
        {new Date(author.dateOfBirth).toLocaleDateString()}
      </p>
      <p>
        <strong>Country:</strong> {author.country}
      </p>
      <Link to={`/editAuthor/${author.id}`} className="btn btn-primary">
        Edit
      </Link>
    </div>
  );
};

export default AuthorDetails;
