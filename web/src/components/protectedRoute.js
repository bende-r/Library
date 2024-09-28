import React from "react";
import { Navigate } from "react-router-dom";
import curUser from "./curUser";

const ProtectedRoute = ({ children, requiredRole }) => {
  const user = curUser.getCurrentUser();
  const role = curUser.getUserRole();

  if (!user || !role || !role.includes(requiredRole)) {
    return <Navigate to="/home" />; // Если пользователь не авторизован или не админ, перенаправляем его на страницу логина
  }

  return children; // Если пользователь соответствует условиям, возвращаем дочерний компонент
};

export default ProtectedRoute;
