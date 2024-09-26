import React, { useState, useEffect } from "react";
import PostService from "../services/post.service";
import AuthService from "../services/auth.service";
import { useNavigate, Link } from "react-router-dom";
import { genreOptions, authorOptions } from "./optionsData";
import handleRefresh from './refresh';

const Books = () => {
  const [books, setBooks] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1); 
  const [selectedGenre, setSelectedGenre] = useState("");
  const [selectedAuthor, setSelectedAuthor] = useState("");
  const [totalPages, setTotalPages] = useState(1);
  const [totalItems, setTotalItems] = useState(0);

  const pageSize = 5;

  const user = AuthService.getCurrentUser();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchBooks = async () => {
      await getBooksWithPagination(currentPage);
    };       
    fetchBooks();
  }, [currentPage]);

  const getBooksWithPagination = async (page) => {
    await PostService.getPagedBooks(page, pageSize).then(
      (response) => {
        setBooks(response.data);
        setTotalPages(response.headers['x-pagination'] ? JSON.parse(response.headers['x-pagination']).TotalPages : 1);
        setTotalItems(response.headers['x-pagination'] ? JSON.parse(response.headers['x-pagination']).TotalCount : 0);
      },
      (error) => {
        if (error.response == null) {
          handleRefresh(user, navigate);
        }
      }  
    );
  };

  const handleGenreChange = (e) => {
    setSelectedGenre(e.target.value);
  };
  
  const handleAuthorChange = (e) => {
    setSelectedAuthor(e.target.value);
  };

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
    setCurrentPage(1); 
  };

  const paginate = (pageNumber) => {
    if (pageNumber >= 1 && pageNumber <= totalPages) {
      setCurrentPage(pageNumber);
    }
  };

  const applyFilters = async (genre, author) => {
    setCurrentPage(1);
    const response = await PostService.getFilteredBooks(genre, author, user.accessToken);
    setBooks(response.data);
    setTotalPages(response.headers['x-pagination'] ? JSON.parse(response.headers['x-pagination']).TotalPages : 1);
    setTotalItems(response.headers['x-pagination'] ? JSON.parse(response.headers['x-pagination']).TotalCount : 0);
  };

  return (
    <div>
      <div className="mt-3">
        <h2>Books</h2>
        <select className="form-select mb-3" value={selectedGenre} onChange={handleGenreChange}>
          {genreOptions.map((option) => (
            <option key={option.value} value={option.value}>{option.label}</option>
          ))}
        </select>
        <select className="form-select mb-3" value={selectedAuthor} onChange={handleAuthorChange}>
          {authorOptions.map((option) => (
            <option key={option.value} value={option.value}>{option.label}</option>
          ))}
        </select>
        <button className="btn btn-primary mb-3" onClick={() => applyFilters(selectedGenre, selectedAuthor)}>
          Apply Filters
        </button>
        <button className="btn btn-primary mb-3" onClick={() => window.location.reload()}>
          Cancel Filters
        </button>
        <div className="input-group mb-3">
          <input
            type="text"
            className="form-control"
            placeholder="Search by title..."
            value={searchTerm}
            onChange={handleSearch}
          />
        </div>
        
        <div className="row row-cols-1 row-cols-md-3 g-4">
          {books.map((book) => (
            <div className="col" key={book.id}>
              <div className="card border-primary">
                <div className="card-header">
                  <Link to={`/bookDetails/${book.id}`}>{book.title}</Link>
                </div>
                <img
                  src={`/pictures/${book.coverImage}`}
                  className="card-img-top"
                  alt={book.title}
                  style={{ objectFit: "cover", height: "300px" }}
                />
                <div className="card-body">
                  <p className="card-text">{book.description}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
        {/* Пагинация */}
        <ul className="pagination justify-content-center">
          {Array.from({ length: totalPages }, (_, i) => (
            <li className={`page-item ${currentPage === i + 1 ? 'active' : ''}`} key={i}>
              <button className="page-link" onClick={() => paginate(i + 1)}>{i + 1}</button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default Books;
