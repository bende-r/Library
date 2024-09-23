import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
    return (
        <header className="header">
            <nav>
                <ul>
                    <li><Link to="/">Главная</Link></li>
                    <li><Link to="/books">Книги</Link></li>
                    <li><Link to="/my-books">Мои книги</Link></li>
                    <li><Link to="/add-book">Добавить книгу</Link></li>
                </ul>
            </nav>
            <div className="auth-links">
                <Link to="/login">Войти</Link> | <Link to="/register">Регистрация</Link>
            </div>
        </header>
    );
};

export default Header;
