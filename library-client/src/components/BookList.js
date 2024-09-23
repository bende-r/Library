import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';

const BookList = () => {
  const [books, setBooks] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  useEffect(() => {
    fetchBooks();
  }, [searchQuery, currentPage]);

  const fetchBooks = async () => {
    try {
      const response = await axios.get('http://localhost:5021/api/books', {
        params: {
          search: searchQuery,
          // page: currentPage,
          // pageSize: 10, // Количество книг на странице
        },
      });
      setBooks(response.data.books);
      setTotalPages(response.data.totalPages);
    } catch (error) {
      console.error("Error fetching books:", error);
    }
  };

  const handleSearch = (e) => {
    setSearchQuery(e.target.value);
    setCurrentPage(1);
  };

  return (
    <div>
      <h2>Books</h2>
      <input
        type="text"
        placeholder="Search by title"
        value={searchQuery}
        onChange={handleSearch}
      />
      <ul>
        {books.length > 0 ? (
          books.map((book) => (
            <li key={book.id}>
              <Link to={`/book/${book.id}`}>{book.title}</Link> - {book.authorName}
            </li>
          ))
        ) : (
          <p>No books found</p>
        )}
      </ul>

      {/* Пагинация */}
      <div>
        {Array.from({ length: totalPages }, (_, index) => (
          <button key={index} onClick={() => setCurrentPage(index + 1)}>
            {index + 1}
          </button>
        ))}
      </div>
    </div>
  );
};

export default BookList;
