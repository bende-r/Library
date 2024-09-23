import React, { useEffect, useState } from 'react';
import BookService from '../../services/BookService';
import { useParams, useNavigate } from 'react-router-dom';

const BookDetail = () => {
    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchBook = async () => {
            const result = await BookService.getBookById(id);
            setBook(result);
            setLoading(false);
        };
        fetchBook();
    }, [id]);

    const handleBorrow = async () => {
        // Логика для заимствования книги
    };

    if (loading) return <div>Загрузка...</div>;
    if (!book) return <div>Книга не найдена</div>;

    return (
        <div>
            <h1>{book.title}</h1>
            <p>{book.author}</p>
            <p>{book.available ? 'Доступно' : 'Недоступно'}</p>
            {book.available && <button onClick={handleBorrow}>Взять книгу</button>}
        </div>
    );
};

export default BookDetail;
