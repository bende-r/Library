import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import BookService from '../../services/BookService';
import Pagination from '../Pagination';

const BookList = () => {
    const [books, setBooks] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const limit = 10; // Количество книг на странице

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const response = await BookService.getBooks(currentPage, limit);
    
                // Проверяем, является ли объектом и есть ли поле items
                if (response && Array.isArray(response.items)) {
                    setBooks(response.items); // Устанавливаем только книги
                } else {
                    console.error('Полученные данные не содержат массив items:', response);
                    setBooks([]);
                }
            } catch (error) {
                console.error('Ошибка при получении книг:', error);
                setBooks([]);
            }
        };
    
        fetchBooks();
    }, [currentPage, limit]);
    
    return (
        <div>
            <h1>Список книг</h1>
            {books.length === 0 ? <p>Книги недоступны</p> : (
                <ul>
                    {books.map(book => (
                        <li key={book.id}>
                            <h2><Link to={`/books/${book.id}`}>{book.title}</Link></h2>
                            <p><strong>ISBN:</strong> {book.isbn}</p>
                            <p><strong>Жанр:</strong> {book.genre}</p>
                            <p><strong>Описание:</strong> {book.description}</p>
                            <p><strong>Автор:</strong> {book.author ? book.author.name : 'Неизвестный автор'}</p>
                            <p><strong>Дата взятия:</strong> {book.borrowedAt ? new Date(book.borrowedAt).toLocaleDateString() : 'Не взята'}</p>
                            <p><strong>Дата возврата:</strong> {book.returnBy ? new Date(book.returnBy).toLocaleDateString() : 'Не указана'}</p>
                        </li>
                    ))}
                </ul>
            )}
            <Pagination currentPage={currentPage} onPageChange={setCurrentPage} />
        </div>
    );
};

export default BookList;
