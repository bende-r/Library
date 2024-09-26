import React from "react";
import { Routes, Route, BrowserRouter } from "react-router-dom";
import Login from "./components/login";
import Books from "./components/books";
import AdminPage from "./components/adminPage";
import Header from './components/Header';
import Register from './components/register';
import Home from './components/home';
import BookDetails from './components/bookDetails';
import EditBook from './components/editBook';
import AddBook from './components/addBook';
import Mybooks from './components/mybooks';

function App() {
  return (
    <div>

      <div className="container-md mt-5">
        <BrowserRouter>
          <Header/>
          <Routes>
          <Route path="/" element={<Home />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/books" element={<Books />} />
            <Route path="/adminPage" element={<AdminPage />} />
            <Route path="/mybooks" element={<Mybooks />} />
            <Route path="/bookDetails/:id" element={<BookDetails />} />
            <Route path="/editBook/:id" element={<EditBook />} />
            <Route path="/addBook" element={<AddBook />} />
          </Routes>
        </BrowserRouter>
      </div>
    </div>
  );
}

export default App;
