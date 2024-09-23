import React, { useState } from 'react';
import AuthService from '../../services/AuthService';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await AuthService.login(email, password);
            // Редирект или изменение состояния после успешного входа
        } catch (error) {
            // Обработка ошибки
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
            <button type="submit">Войти</button>
        </form>
    );
};

export default Login;
