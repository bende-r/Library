import axios from "axios";
import authHeader from "./auth-header";

const baseURL = "http://localhost:5021";

const API_URL = "/api/";

const getAllBooks = () => {
  return axios
    .get(baseURL + API_URL + "books/books", { headers: authHeader() })
    .catch((error) => {
      throw error;
    });
};

const getBookById = (BookId) => {
  return axios
    .get(baseURL + API_URL + "books/" + BookId, { headers: authHeader() })
    .catch((error) => {
      throw error;
    });
};

const deleteBook = (BookId) => {
  return axios
    .delete(baseURL + API_URL + "books/" + BookId, { headers: authHeader() })
    .catch((error) => {
      throw error;
    });
};

const updateBook = (formData) => {
  return axios
    .put(baseURL + API_URL + "books/" + formData.id, formData, {
      headers: authHeader(),
    })
    .catch((error) => {
      throw error;
    });
};

const addBook = (formData) => {
  return axios
    .post(baseURL + API_URL + "books", formData, { headers: authHeader() })
    .catch((error) => {
      throw error;
    });
};

const getFilteredBooks = (category, place) => {
  return axios
    .post(
      baseURL + API_URL + "Books/getFilteredBooks",
      { category, place },
      { headers: authHeader() }
    )
    .catch((error) => {
      throw error;
    });
};

const uploadPicture = (formDataWithImage) => {
  return axios
    .post(baseURL + API_URL + "Books/uploadPicture", formDataWithImage, {
      headers: authHeader(),
    })
    .catch((error) => {
      throw error;
    });
};

const registerOnBook = (userId, BookId) => {
  return axios
    .post(
      baseURL + API_URL + "books/borrow/",
      { userId, BookId },
      { headers: authHeader() }
    )
    .then((response) => {
      return response;
    })
    .catch((error) => {
      throw error;
    });
};

const unregisterFromBook = (userId, BookId) => {
  return axios
    .post(
      baseURL + API_URL + "Books/unRegisterOnBook/",
      { userId, BookId },
      { headers: authHeader() }
    )
    .then((response) => {
      return response;
    })
    .catch((error) => {
      throw error;
    });
};

const getUsersBooks = (userId) => {
  return axios
    .get(baseURL + API_URL + "Books/getUserBooks/" + userId, {
      headers: authHeader(),
    })
    .then((response) => {
      return response;
    })
    .catch((error) => {
      throw error;
    });
};

const getPagedBooks = (pageNumber, pageSize) => {
  return axios
    .post(
      baseURL + API_URL + "Books/getPagedBooks",
      { pageNumber, pageSize },
      { headers: authHeader() }
    )
    .then((response) => {
      return response;
    })
    .catch((error) => {
      throw error;
    });
};

const getAllAuthors = () => {
  return axios
    .get(baseURL + API_URL + "authors", { headers: authHeader() })
    .catch((error) => {
      throw error;
    });
};

const borrowBook = (userId, bookId, borrowDate, returnDate) => {
  return axios
    .post(
      baseURL + API_URL + "books/borrow",
      {
        userId,
        bookId,
        borrowDate,
        returnDate,
      },
      { headers: authHeader() }
    )
    .then((response) => {
      return response;
    })
    .catch((error) => {
      throw error;
    });
};

const returnBook = (userId, bookId) => {
  return axios
    .post(
      baseURL + API_URL + "books/return",
      {
        userId: userId,
        bookId: bookId,
      },
      { headers: authHeader() }
    )
    .catch((error) => {
      throw error;
    });
};

const postService = {
  getAllBooks,
  getBookById,
  updateBook,
  addBook,
  deleteBook,
  getFilteredBooks,
  uploadPicture,
  registerOnBook,
  unregisterFromBook,
  getUsersBooks,
  getPagedBooks,
  getAllAuthors,
  borrowBook,
  returnBook,
};

export default postService;
