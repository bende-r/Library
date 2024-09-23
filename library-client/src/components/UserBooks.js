import React, { useState, useEffect } from 'react';
import axios from 'axios';

const UserBooks = () => {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    fetchUserBooks();
  }, []);

  const fetchUserBooks = async () => {
    const response = await axios.get('http://localhost:5000/api/user/books', {
      headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
    });
    setBooks(response.data);
  };

  return (
    <div>
      <h2>Your Borrowed Books</h2>
      <ul>
        {books.map((book) => (
          <li key={book.id}>{book.title}</li>
        ))}
      </ul>
    </div>
  );
};

export default UserBooks;
