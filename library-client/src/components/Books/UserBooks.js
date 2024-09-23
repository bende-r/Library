import React, { useEffect, useState } from 'react';
import UserService from '../../services/UserService';

const UserBooks = () => {
    const [userBooks, setUserBooks] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUserBooks = async () => {
            const result = await UserService.getUserBooks();
            setUserBooks(result);
            setLoading(false);
        };
        fetchUserBooks();
    }, []);

    if (loading) return <div>Загрузка...</div>;

    return (
        <div>
            <h1>Ваши книги</h1>
            {userBooks.length === 0 ? <p>Вы не взяли никаких книг</p> : (
                <ul>
                    {userBooks.map(book => (
                        <li key={book.id}>{book.title}</li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default UserBooks;
