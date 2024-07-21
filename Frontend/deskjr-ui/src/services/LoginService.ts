import axios from 'axios';

const baseUrl = 'https://localhost:7187/api/Login';

class LoginService {
  public async login(id: string, password: string) {
    const response = await axios.post(`${baseUrl}/login`, {
      id,
      password,
    });
    return response.data;
  }
}

export default new LoginService();
