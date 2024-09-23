import React from 'react';
import { Link } from 'react-router-dom';
import AppRoutes from './routes';  // Импортируем определённые маршруты
import './App.css';  // Стили для приложения, если нужно

const App = () => {
  return (
    <div className="App">
      {/* Навигационное меню */}
      <header>
        <nav>
          <ul>
            <li><Link to="/">Books</Link></li>
            <li><Link to="/authors">Authors</Link></li>
            <li><Link to="/login">Login</Link></li>
            <li><Link to="/register">Register</Link></li>
          </ul>
        </nav>
      </header>

      {/* Здесь рендерятся компоненты, в зависимости от маршрута */}
      <main>
        <AppRoutes />
      </main>

      {/* Футер */}
      <footer>
        <p>Library App © 2024</p>
      </footer>
    </div>
  );
};

export default App;
