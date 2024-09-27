import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import PostService from "../services/post.service";
import AuthService from "../services/auth.service";
import handleRefresh from './refresh';

const EditBook = () => {
  const user = AuthService.getCurrentUser();
  const { id } = useParams();
  const [errorMessage, setErrorMessage] = useState('');
  const [formData, setFormData] = useState({
    id: "",
    isbn: "",
    title: "",
    genre: "",
    description: "",
    authorId: "", // Используем authorId вместо имени автора
    isBorrowed: false, 
    picture: "",
  });
  const [authors, setAuthors] = useState([]); // Массив для хранения списка авторов
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const [imageFile, setImageFile] = useState(null);

  const handleImageChange = (e) => {
    setImageFile(e.target.files[0]);
  };

  // Получаем список авторов при загрузке страницы
  useEffect(() => {
    const fetchAuthors = async () => {
      try {
        const response = await PostService.getAllAuthors(); // Метод для получения списка авторов
        setAuthors(response.data); // Предполагаем, что возвращается массив авторов с полем id и name
      } catch (error) {
        console.error("Error fetching authors:", error);
      }
    };

    const fetchBookDetails = async () => {
      try {
        await PostService.getBookById(id).then((response) => {
          setFormData({
            id: response.data.id,
            title: response.data.title,
            description: response.data.description,
            authorId: response.data.authorId, // Используем authorId
            isbn: response.data.isbn,
            genre: response.data.genre,
            picture: response.data.picture,
            isBorrowed: response.data.isBorrowed
          });
          setLoading(false);
        },
        (error) => {
          if (error.response == null) {
            handleRefresh(user, navigate);
          } else {
            alert(error.response.data['ErrorMessage']);
          }
        });
      } catch (err) {
        setLoading(false);
        console.error("Error fetching book details:", err);
      }
    };

    fetchAuthors(); // Получаем авторов
    fetchBookDetails(); // Получаем информацию о книге
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
    const formDataWithImage = new FormData();
    formDataWithImage.append('file', imageFile); 
    formDataWithImage.append('bookId', formData.id);

    try {
      if (imageFile != null) {
        await PostService.uploadPicture(formDataWithImage).then(
          (response) => {
            PostService.updateBook({ ...formData, picture: response.data }).then(
              (response) => {
                navigate("/books");
              },
              (error) => {
                setErrorMessage(error.response.data['ErrorMessage']);
              }
            );
          }
        );
      } else {
        await PostService.updateBook(formData).then(
          (response) => {
            navigate("/books");
          },
          (error) => {
            setErrorMessage(error.response.data['ErrorMessage']);
          }
        );
      }
    } catch (error) {
      console.error("Error updating book details:", error);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="container mt-4">
      <h2>Edit Book</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="title" className="form-label">Title:</label>
          <input type="text" className="form-control" id="title" name="title" value={formData.title} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="description" className="form-label">Description:</label>
          <textarea className="form-control" id="description" name="description" value={formData.description} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="authorId" className="form-label">Author:</label>
          <select className="form-control" id="authorId" name="authorId" value={formData.authorId} onChange={handleInputChange} required>
            <option value="">Select Author</option>
            {authors.map((author) => (
              <option key={author.id} value={author.id}>{author.name}</option>
            ))}
          </select>
        </div>
        <div className="mb-3">
          <label htmlFor="isbn" className="form-label">ISBN:</label>
          <input type="text" className="form-control" id="isbn" name="isbn" value={formData.isbn} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="genre" className="form-label">Genre:</label>
          <input type="text" className="form-control" id="genre" name="genre" value={formData.genre} onChange={handleInputChange} required />
        </div>
        <div className="mb-3">
          <label htmlFor="photo" className="form-label">Cover Image:</label>
          <input type="file" className="form-control" id="photo" name="photo" onChange={handleImageChange} accept="image/*" />
        </div>
        <button type="submit" className="btn btn-primary">Update Book</button>
      </form>
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
};

export default EditBook;
