import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

const BookForm = () => {
  const { id } = useParams();
  const [book, setBook] = useState({ title: '', description: '', isbn: '', genre: '' });
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      fetchBook();
    }
  }, [id]);

  const fetchBook = async () => {
    const response = await axios.get(`http://localhost:5000/api/books/${id}`);
    setBook(response.data);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (id) {
      await axios.put(`http://localhost:5000/api/books/${id}`, book, {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
    } else {
      await axios.post('http://localhost:5000/api/books', book, {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
    }
    navigate('/');
  };

  return (
    <div>
      <h2>{id ? 'Edit Book' : 'Add New Book'}</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Title"
          value={book.title}
          onChange={(e) => setBook({ ...book, title: e.target.value })}
        />
        <textarea
          placeholder="Description"
          value={book.description}
          onChange={(e) => setBook({ ...book, description: e.target.value })}
        />
        <input
          type="text"
          placeholder="ISBN"
          value={book.isbn}
          onChange={(e) => setBook({ ...book, isbn: e.target.value })}
        />
        <input
          type="text"
          placeholder="Genre"
          value={book.genre}
          onChange={(e) => setBook({ ...book, genre: e.target.value })}
        />
        <button type="submit">{id ? 'Update' : 'Add'}</button>
      </form>
    </div>
  );
};

export default BookForm;
