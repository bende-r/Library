import axios from 'axios';

// Создаем экземпляр Axios с базовым URL
const api = axios.create({
  baseURL: 'http://localhost:5000/api', // Базовый URL для всех запросов
});

// Добавляем интерсептор для добавления токена в заголовки запросов
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token'); // Получаем токен из localStorage
  if (token) {
    config.headers.Authorization = `Bearer ${token}`; // Добавляем токен в заголовки
  }
  return config;
});

// Методы работы с книгами
export const getBooks = async (params = {}) => {
  const response = await api.get('/books', { params });
  return response.data;
};

export const getBookById = async (id) => {
  const response = await api.get(`/books/${id}`);
  return response.data;
};

export const addBook = async (book) => {
  const response = await api.post('/books', book);
  return response.data;
};

export const updateBook = async (id, book) => {
  const response = await api.put(`/books/${id}`, book);
  return response.data;
};

export const deleteBook = async (id) => {
  await api.delete(`/books/${id}`);
};

// Загрузка изображения книги
export const uploadBookImage = async (id, imageFile) => {
  const formData = new FormData();
  formData.append('image', imageFile);

  const response = await api.post(`/books/${id}/upload`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response.data;
};

// Выдача книги пользователю
export const issueBookToUser = async (bookId, userId) => {
  const response = await api.post(`/books/${bookId}/issue`, { userId });
  return response.data;
};

// Отправка уведомлений об истечении срока возврата книги
export const sendOverdueNotification = async (bookId) => {
  const response = await api.post(`/books/${bookId}/notify-overdue`);
  return response.data;
};

// Методы работы с авторами
export const getAuthors = async () => {
  const response = await api.get('/authors');
  return response.data;
};

export const getAuthorById = async (id) => {
  const response = await api.get(`/authors/${id}`);
  return response.data;
};

export const addAuthor = async (author) => {
  const response = await api.post('/authors', author);
  return response.data;
};

export const updateAuthor = async (id, author) => {
  const response = await api.put(`/authors/${id}`, author);
  return response.data;
};

export const deleteAuthor = async (id) => {
  await api.delete(`/authors/${id}`);
};

// Авторизация
export const login = async (credentials) => {
  const response = await api.post('/auth/login', credentials);
  localStorage.setItem('token', response.data.token); // Сохраняем токен в localStorage
  return response.data;
};

export const register = async (credentials) => {
  const response = await api.post('/auth/register', credentials);
  return response.data;
};

// Получение книг, взятых пользователем
export const getUserBooks = async () => {
  const response = await api.get('/user/books');
  return response.data;
};
