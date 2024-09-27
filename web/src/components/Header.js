import React from 'react';
import AuthService from "../services/auth.service";
import { useNavigate, Link } from "react-router-dom";
import '../css/Header.css'; 
import curUser from './curUser';

const Header = () => {
  const user = curUser.getCurrentUser();
  console.log('User:', user);
  const role = curUser.getUserRole();
  console.log('User role:', role); 

  const navigate = useNavigate();

  const logOut = () => {
    AuthService.logout();
    navigate("/home");
    window.location.reload();
  };
  
  
  return (
    <header>
      <nav>
        <ul className="navbar">
          {user ? (
            <>
              <li>
                <Link to="/">Home</Link>
              </li>
              <li>
                <Link to="/books">Books</Link>
              </li>
              <li>
                <Link to="/myBooks">My Books</Link>
              </li>
              {role && role.includes("Admin") && ( 
                <li>
                  <Link to="/adminPage">Admin Panel</Link>
                </li>
              )}
              <li>
                <span>{user.user.email}</span> {/* Отображение имени пользователя */}
              </li>
              <li>
                <button className="logout-button" onClick={logOut}>Logout</button>
              </li>
            </>
          ) : (
            <>
              <li>
                <Link to="/login">Login</Link>
              </li>
              <li>
                <Link to="/register">Register</Link>
              </li>
            </>
          )}
        </ul>
      </nav>
    </header>
  );
};

export default Header;
