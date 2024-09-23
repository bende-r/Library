import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import BookList from './components/BookList';
import BookDetail from './components/BookDetail';
import BookForm from './components/BookForm';
import UserBooks from './components/UserBooks';
import LoginForm from './components/LoginForm';
import RegisterForm from './components/RegisterForm';
import AuthorList from './components/AuthorList';  // Добавляем импорт
import AuthorForm from './components/AuthorForm';  // Добавляем импорт

const AppRoutes = () => {
  return (
    
      <Routes>
        <Route path="/" element={<BookList />} />
        <Route path="/book/:id" element={<BookDetail />} />
        <Route path="/admin/book/new" element={<BookForm />} />
        <Route path="/admin/book/edit/:id" element={<BookForm />} />
        <Route path="/user/books" element={<UserBooks />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<RegisterForm />} />
        <Route path="/authors" element={<AuthorList />} /> {/* Исправлено */}
        <Route path="/admin/author/new" element={<AuthorForm />} /> {/* Исправлено */}
        <Route path="/admin/author/edit/:id" element={<AuthorForm />} /> {/* Исправлено */}
      </Routes>
    
  );
};

export default AppRoutes;
