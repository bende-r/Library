import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getBooks } from '../services/BookService';
import Pagination from './Pagination';

const BookList = () => {
    const [books, setBooks] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const limit = 10; // Количество книг на странице

    useEffect(() => {
        const fetchBooks = async () => {
            const response = await getBooks(currentPage, limit);
            if (Array.isArray(response)) {
                setBooks(response);
            } else {
                console.error('Полученные данные не являются массивом:', response);
                setBooks([]);
            }
        };

        fetchBooks();
    }, [currentPage]);

    return (
        <div>
            <h1>Список книг</h1>
            {books.length === 0 ? <p>Книги недоступны</p> : (
                <ul>
                    {books.map(book => (
                        <li key={book.id}>
                            <Link to={`/books/${book.id}`}>{book.title}</Link>
                        </li>
                    ))}
                </ul>
            )}
            <Pagination currentPage={currentPage} onPageChange={setCurrentPage} />
        </div>
    );
};

export default BookList;
