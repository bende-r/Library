import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

const AuthService = {
    login: async (email, password) => {
        const response = await axios.post(`${API_URL}/auth/login`, { email, password });
        return response.data;
    },
    register: async (email, password) => {
        const response = await axios.post(`${API_URL}/auth/register`, { email, password });
        return response.data;
    },
};

export default AuthService;
