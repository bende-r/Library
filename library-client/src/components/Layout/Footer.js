import React from 'react';

const Footer = () => {
    return (
        <footer className="footer">
            <p>&copy; {new Date().getFullYear()} Библиотека. Все права защищены.</p>
            <p>Контакты: <a href="mailto:support@library.com">support@library.com</a></p>
        </footer>
    );
};

export default Footer;
