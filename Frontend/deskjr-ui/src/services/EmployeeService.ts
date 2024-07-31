import api from "../utils/axiosConfig";

const baseUrl = "/api/Employee";

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
}

export default new EmployeeService();
