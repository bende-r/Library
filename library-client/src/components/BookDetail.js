import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { getBookById, deleteBook } from '../services/api';
import { getUserRole } from '../services/auth';  // Получаем роль пользователя

const BookDetail = () => {
  const { id } = useParams();
  const [book, setBook] = useState(null);
  const navigate = useNavigate();  // Для редиректа после удаления

  useEffect(() => {
    fetchBook();
  }, [id]);

  const fetchBook = async () => {
    const result = await getBookById(id);
    setBook(result);
  };

  const handleDelete = async () => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      await deleteBook(id);
      navigate('/');  // Перенаправляем пользователя на главную страницу после удаления
    }
  };

  if (!book) return <div>Loading...</div>;  // Отображаем загрузку, если книга не загружена

  return (
    <div>
      <h1>{book.title}</h1>
      <p>{book.description}</p>
      <p>Author: {book.authorName}</p>

      {/* Админские кнопки */}
      {getUserRole() === 'Admin' && (
        <>
          <Link to={`/admin/book/edit/${id}`}>Edit</Link>
          <button onClick={handleDelete}>Delete</button>
        </>
      )}
    </div>
  );
};

export default BookDetail;
