import React from "react";
import { Routes, Route, BrowserRouter } from "react-router-dom";
import Login from "./components/login";
import Books from "./components/books";
import AdminPage from "./components/adminPage";
import Header from "./components/Header";
import Register from "./components/register";
import Home from "./components/home";
import BookDetails from "./components/bookDetails";
import EditBook from "./components/editBook";
import AddBook from "./components/addBook";
import Mybooks from "./components/mybooks";
import ProtectedRoute from "./components/protectedRoute";
import AddAuthor from "./components/AddAuthor"; // Компонент добавления автора
import EditAuthor from "./components/EditAuthor"; // Компонент редактирования автора
import AuthorDetails from "./components/AuthorDetails"; // Компонент просмотра автора
import AuthorsPage from "./components/AuthorsPage";

function App() {
  return (
    <div>
      <div className="container-md mt-5">
        <BrowserRouter>
          <Header />
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/books" element={<Books />} />
            <Route path="/mybooks" element={<Mybooks />} />
            <Route path="/bookDetails/:id" element={<BookDetails />} />
            <Route
              path="/adminPage"
              element={
                <ProtectedRoute requiredRole="Admin">
                  <AdminPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/addBook"
              element={
                <ProtectedRoute requiredRole="Admin">
                  <AddBook />
                </ProtectedRoute>
              }
            />
            <Route
              path="/editBook/:id"
              element={
                <ProtectedRoute requiredRole="Admin">
                  <EditBook />
                </ProtectedRoute>
              }
            />

            {/* Маршрут для добавления автора */}
            <Route
              path="/authors"
              element={
                <ProtectedRoute requiredRole="Admin">
                  <AuthorsPage />
                </ProtectedRoute>
              }
            />

            {/* Маршрут для добавления автора */}
            <Route
              path="/addAuthor"
              element={
                <ProtectedRoute requiredRole="Admin">
                  <AddAuthor />
                </ProtectedRoute>
              }
            />

            {/* Маршрут для редактирования автора */}
            <Route
              path="/editAuthor/:id"
              element={
                <ProtectedRoute requiredRole="Admin">
                  <EditAuthor />
                </ProtectedRoute>
              }
            />

            {/* Маршрут для просмотра деталей автора */}
            <Route
              path="/authorDetails/:id"
              element={
                <ProtectedRoute requiredRole="Admin">
                  <AuthorDetails />
                </ProtectedRoute>
              }
            />
          </Routes>
        </BrowserRouter>
      </div>
    </div>
  );
}

export default App;
