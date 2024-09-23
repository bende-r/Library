import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import App from './App';  // Основной компонент приложения
import './index.css';  // Стили для всего приложения

const root = ReactDOM.createRoot(document.getElementById('root')); // Связываем React с DOM-элементом "root"

root.render(
  <React.StrictMode>
    {/* Включаем маршрутизацию в приложении */}
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </React.StrictMode>
);
