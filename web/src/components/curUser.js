import { jwtDecode } from "jwt-decode";


const curUser = {
  getCurrentUser: () => {
    const user = JSON.parse(localStorage.getItem('user')); // Предполагается, что объект user хранится в localStorage
    if (!user || !user.token) return null;

    return user;
  },

  getUserRole: () => {
    const user = curUser.getCurrentUser();
    if (!user) return null;

    const decodedToken = jwtDecode(user.token); // Декодируем токен
    return decodedToken.role; // Возвращаем роль пользователя
  },

  logout: () => {
    localStorage.removeItem('user'); // Удаление данных пользователя
  }
};

export default curUser;
