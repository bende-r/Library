import { jwtDecode } from 'jwt-decode'; // Используем именованный импорт

export const getUserRole = () => {
  const token = localStorage.getItem('token');
  if (!token) return null;

  const decodedToken = jwtDecode(token);
  return decodedToken.role; // Убедись, что в токене есть информация о роли пользователя
};
