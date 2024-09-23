import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAuthors, deleteAuthor } from '../services/api';
import { getUserRole } from '../services/auth'; // Получение роли пользователя

const AuthorList = () => {
  const [authors, setAuthors] = useState([]);
  const userRole = getUserRole(); // Получаем роль пользователя

  useEffect(() => {
    fetchAuthors();
  }, []);

  const fetchAuthors = async () => {
    const response = await getAuthors();
    setAuthors(response);
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this author?')) {
      await deleteAuthor(id);
      fetchAuthors(); // Обновляем список после удаления
    }
  };

  return (
    <div>
      <h2>Author List</h2>
      <ul>
        {authors.map((author) => (
          <li key={author.id}>
            {author.firstName} {author.lastName}
            {/* Только админ может редактировать и удалять */}
            {userRole === 'Admin' && (
              <>
                <Link to={`/admin/author/edit/${author.id}`}>Edit</Link>
                <button onClick={() => handleDelete(author.id)}>Delete</button>
              </>
            )}
          </li>
        ))}
      </ul>
      {/* Кнопка добавления автора только для админа */}
      {userRole === 'Admin' && <Link to="/admin/author/new">Add New Author</Link>}
    </div>
  );
};

export default AuthorList;
