import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getAuthorById, addAuthor, updateAuthor } from '../services/api';

const AuthorForm = () => {
  const { id } = useParams(); // Получаем id автора, если редактируем
  const [author, setAuthor] = useState({ firstName: '', lastName: '', country: '', birthDate: '' });
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      fetchAuthor();
    }
  }, [id]);

  const fetchAuthor = async () => {
    const response = await getAuthorById(id);
    setAuthor(response);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (id) {
      await updateAuthor(id, author);
    } else {
      await addAuthor(author);
    }
    navigate('/authors'); // Перенаправляем на список авторов после сохранения
  };

  return (
    <div>
      <h2>{id ? 'Edit Author' : 'Add New Author'}</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="First Name"
          value={author.firstName}
          onChange={(e) => setAuthor({ ...author, firstName: e.target.value })}
          required
        />
        <input
          type="text"
          placeholder="Last Name"
          value={author.lastName}
          onChange={(e) => setAuthor({ ...author, lastName: e.target.value })}
          required
        />
        <input
          type="text"
          placeholder="Country"
          value={author.country}
          onChange={(e) => setAuthor({ ...author, country: e.target.value })}
          required
        />
        <input
          type="date"
          value={author.birthDate}
          onChange={(e) => setAuthor({ ...author, birthDate: e.target.value })}
          required
        />
        <button type="submit">{id ? 'Update Author' : 'Add Author'}</button>
      </form>
    </div>
  );
};

export default AuthorForm;
