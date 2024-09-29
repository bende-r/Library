import React, { useState, useEffect } from "react";
import PostService from "../services/post.service";
import curUser from "./curUser";
import { useNavigate, Link } from "react-router-dom";
import { genreOptions } from "./optionsData";
import handleRefresh from "./refresh";

const Books = () => {
  const [books, setBooks] = useState([]);
  const [filteredBooks, setFilteredBooks] = useState([]); // Состояние для отфильтрованных книг
  const [searchTerm, setSearchTerm] = useState("");
  const [isbnSearchTerm, setIsbnSearchTerm] = useState(""); // Новое состояние для поиска по ISBN
  const [selectedGenre, setSelectedGenre] = useState("");
  const [selectedAuthor, setSelectedAuthor] = useState("");
  const [authors, setAuthors] = useState([]); // Состояние для списка авторов
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalItems, setTotalItems] = useState(0);

  const pageSize = 6;
  const role = curUser.getUserRole(); // Получаем информацию о пользователе
  const isAdmin = role && role.includes("Admin"); // Проверка роли администратора
  const navigate = useNavigate();

  useEffect(() => {
    const fetchBooks = async () => {
      await getBooksWithPagination(currentPage);
    };
    fetchBooks();
  }, [currentPage]);

  useEffect(() => {
    // Получаем список авторов при загрузке страницы
    const fetchAuthors = async () => {
      try {
        const response = await PostService.getAllAuthors();
        setAuthors(response.data);
      } catch (error) {
        console.error("Error fetching authors:", error);
      }
    };
    fetchAuthors();
  }, []);

  const getBooksWithPagination = async (page) => {
    await PostService.getPagedBooks(page, pageSize).then(
      (response) => {
        let booksData = response.data;
        console.log(response);
        setBooks(booksData);
        setFilteredBooks(booksData);
        setTotalPages(
          response.headers["x-pagination"]
            ? JSON.parse(response.headers["x-pagination"]).TotalPages
            : 1
        );
        setTotalItems(
          response.headers["x-pagination"]
            ? JSON.parse(response.headers["x-pagination"]).TotalCount
            : 0
        );
      },
      (error) => {
        if (error.response == null) {
          handleRefresh(role, navigate);
        }
      }
    );
  };

  // Поиск по названию
  const handleSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
    applyFilters(term, isbnSearchTerm, selectedGenre, selectedAuthor);
  };

  // Поиск по ISBN
  const handleIsbnSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setIsbnSearchTerm(term);
    applyFilters(searchTerm, term, selectedGenre, selectedAuthor);
  };

  // Фильтрация по жанру
  const handleGenreChange = (e) => {
    const genre = e.target.value;
    setSelectedGenre(genre);
    applyFilters(searchTerm, isbnSearchTerm, genre, selectedAuthor);
  };

  // Фильтрация по автору
  const handleAuthorChange = (e) => {
    const author = e.target.value;
    setSelectedAuthor(author);
    applyFilters(searchTerm, isbnSearchTerm, selectedGenre, author);
  };

  // Применение фильтров
  const applyFilters = (searchTerm, isbnSearchTerm, genre, author) => {
    const filtered = books.filter((book) => {
      const matchesSearch = book.title.toLowerCase().includes(searchTerm);
      const matchesIsbn =
        isbnSearchTerm === "" ||
        book.isbn.toLowerCase().includes(isbnSearchTerm);
      const matchesGenre = genre === "" || book.genre === genre;
      const matchesAuthor = author === "" || book.authorId === author;
      return matchesSearch && matchesIsbn && matchesGenre && matchesAuthor;
    });
    setFilteredBooks(filtered);
  };

  const paginate = (pageNumber) => {
    if (pageNumber >= 1 && pageNumber <= totalPages) {
      setCurrentPage(pageNumber);
    }
  };

  return (
    <div>
      <div className="mt-3">
        <h2>Books</h2>
        {/* Фильтр по жанру */}
        <select
          className="form-select mb-3"
          value={selectedGenre}
          onChange={handleGenreChange}
        >
          {genreOptions.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
        {/* Фильтр по автору */}
        <select
          className="form-select mb-3"
          value={selectedAuthor}
          onChange={handleAuthorChange}
        >
          <option value="">Select Author...</option>
          {authors.map((author) => (
            <option key={author.id} value={author.id}>
              {author.firstName} {author.lastName}
            </option>
          ))}
        </select>
        {/* Поиск по названию */}
        <div className="input-group mb-3">
          <input
            type="text"
            className="form-control"
            placeholder="Search by title..."
            value={searchTerm}
            onChange={handleSearch}
          />
        </div>
        {/* Поиск по ISBN */}
        <div className="input-group mb-3">
          <input
            type="text"
            className="form-control"
            placeholder="Search by ISBN..."
            value={isbnSearchTerm}
            onChange={handleIsbnSearch} // Поиск по ISBN
          />
        </div>

        <div className="row row-cols-1 row-cols-md-3 g-4">
          {filteredBooks.map((book) => (
            <div className="col" key={book.id}>
              <div className="card border-primary">
                <div className="card-header">
                  <Link to={`/bookDetails/${book.id}`}>{book.title}</Link>
                </div>
                <img
                  src={`/pictures/${book.imageUrl || "default.jpg"}`}
                  className="card-img-top"
                  alt={book.title}
                  style={{ objectFit: "cover", height: "300px" }}
                />
                <div className="card-body">
                  <p className="card-text">
                    <strong>Title:</strong> {book.title}
                  </p>
                  <p className="card-text">
                    <strong>Author:</strong> {book.author.firstName}{" "}
                    {book.author.lastName}
                  </p>
                  <p className="card-text">
                    <strong>ISBN:</strong> {book.isbn}
                  </p>
                  <p className="card-text">
                    <strong>Genre:</strong> {book.genre}
                  </p>
                  <p className="card-text">
                    <strong>Description:</strong> {book.description}
                  </p>

                  <p className="card-text">
                    <strong>Is Borrowed:</strong>{" "}
                    {book.isBorrowed ? "Not available" : "No"}
                  </p>
                </div>
              </div>
            </div>
          ))}
        </div>
        {/* Пагинация */}
        <ul className="pagination justify-content-center">
          {Array.from({ length: totalPages }, (_, i) => (
            <li
              className={`page-item ${currentPage === i + 1 ? "active" : ""}`}
              key={i}
            >
              <button className="page-link" onClick={() => paginate(i + 1)}>
                {i + 1}
              </button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default Books;
