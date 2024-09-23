import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

const BookService = {
    getBooks: async (page, limit) => {
        const response = await axios.get(`${API_URL}/books?page=${page}&limit=${limit}`);
        return response.data;
    },
    getBookById: async (id) => {
        const response = await axios.get(`${API_URL}/books/${id}`);
        return response.data;
    },
    addBook: async (book) => {
        const response = await axios.post(`${API_URL}/books`, book);
        return response.data;
    },
    updateBook: async (id, book) => {
        const response = await axios.put(`${API_URL}/books/${id}`, book);
        return response.data;
    },
    deleteBook: async (id) => {
        const response = await axios.delete(`${API_URL}/books/${id}`);
        return response.data;
    },
};

export default BookService;
