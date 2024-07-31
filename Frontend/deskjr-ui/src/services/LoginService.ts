import { SET_ERROR } from '../store/actions/actionTypes';
import { SetErrorAction } from '../store/actions/errorActions';
import { ErrorResponseDto } from '../types/ErrorResponseDto';
import api from '../utils/axiosConfig';

export const setError = (error: ErrorResponseDto): SetErrorAction => ({
  type: SET_ERROR,
  payload: error,
});

const baseUrl = '/Login';

class LoginService {
  public async login(body: any) {
    try {
      const response = await api.post(baseUrl, body);
      return response.data; 
    }
    catch(error) {
      const ErrorResponseDto = setError(error);
      if(ErrorResponseDto){
        console.error('Error during login: ', ErrorResponseDto);
        throw ErrorResponseDto;
      }
      throw error;
    }    
  }
}
export default new LoginService();
