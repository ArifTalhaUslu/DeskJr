import axios from 'axios';

const baseUrl = 'https://localhost:7187/api/Login';

class LoginService {
    public async login(body: any) {
        const response = await axios.post(`${baseUrl}`, body);
    return response.data;
  }
}

export default new LoginService();
