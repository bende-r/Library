import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Layout/Header';
import Footer from './components/Layout/Footer';
import Login from './components/Auth/Login.js';
import Register from './components/Auth/Register';
import BookList from './components/Books/BookList';
import BookDetail from './components/Books/BookDetail';
import AddEditBook from './components/Books/AddEditBook';
import UserBooks from './components/Books/UserBooks';

const App = () => {
    return (
        <Router>
            <Header />
            <div className="content">
                <Routes>
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/books" element={<BookList />} />
                    <Route path="/books/:id" element={<BookDetail />} />
                    <Route path="/add-book" element={<AddEditBook />} />
                    <Route path="/edit-book/:id" element={<AddEditBook />} />
                    <Route path="/my-books" element={<UserBooks />} />
                </Routes>
            </div>
            <Footer />
        </Router>
    );
};

export default App;
