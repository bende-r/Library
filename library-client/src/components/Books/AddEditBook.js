import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import BookService from '../../services/BookService';

const AddEditBook = () => {
    const { id } = useParams();
    const [book, setBook] = useState({ title: '', author: '', available: true });
    const navigate = useNavigate();

    useEffect(() => {
        if (id) {
            const fetchBook = async () => {
                const result = await BookService.getBookById(id);
                setBook(result);
            };
            fetchBook();
        }
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (id) {
            await BookService.updateBook(id, book);
        } else {
            await BookService.addBook(book);
        }
        navigate('/books');
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={book.title}
                onChange={(e) => setBook({ ...book, title: e.target.value })}
                required
            />
            <input
                type="text"
                value={book.author}
                onChange={(e) => setBook({ ...book, author: e.target.value })}
                required
            />
            <label>
                Доступно:
                <input
                    type="checkbox"
                    checked={book.available}
                    onChange={(e) => setBook({ ...book, available: e.target.checked })}
                />
            </label>
            <button type="submit">{id ? 'Сохранить изменения' : 'Добавить книгу'}</button>
        </form>
    );
};

export default AddEditBook;
