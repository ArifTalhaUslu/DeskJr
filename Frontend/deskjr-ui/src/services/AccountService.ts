import api from '../utils/axiosConfig';

const accountBaseUrl = "/api/Account";

class AccountService {
  
    public async changePassword(changePasswordRequest: any, currentUser: any) {
        if (!currentUser || !currentUser.id) {
          throw new Error("User email is not available");
        }
        const response = await api.post(`${accountBaseUrl}/change-password`, {
          ...changePasswordRequest,
          id: currentUser.id,
        });
        return response.data;
      }
}

export default new AccountService();
