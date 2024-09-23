import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

const UserService = {
    getUserBooks: async () => {
        const response = await axios.get(`${API_URL}/user/books`);
        return response.data;
    },
};

export default UserService;
