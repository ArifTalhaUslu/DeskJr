import { SET_ERROR } from '../store/actions/actionTypes';
import { ErrorResponseDto } from '../types/ErrorResponseDto';
import { SetErrorAction } from '../store/actions/errorActions';
import api from '../utils/axiosConfig';

const baseUrl = "/api/Employee";

export const setError = (error: ErrorResponseDto): SetErrorAction => ({
    type: SET_ERROR,
    payload: error,
  });


class EmployeeService {
  public async getAllEmployee() {
    const response = await api.get(baseUrl);
    return response.data;
  }

  public async getEmployeeById(id: any) {
    const response = await api.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateEmployee(employee: any) {
    const response = await api.post(baseUrl, employee);
    return response.data;
  }

  public async updateEmployee(employee: any) {
    const response = await api.put(baseUrl, employee);
    return response.data;
  }

  public async deleteEmployee(id: any) {
    const response = await api.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
  
  public async getUpcomingBirthdays() {
    const response = await api.get(`${baseUrl}/upcoming-birthdays`);
    return response.data;
  }
  
  public async getEmployeesByManagerId(managerId: any) {
    const response = await api.get(`${baseUrl}/employeesByManagerId/${managerId}`);
    return response.data;
  }
  
}

export default new EmployeeService();
