import api from "../utils/axiosConfig";
const baseUrl = "/api/EmployeeTitle";

class EmployeeTitleService {
  public async getAllEmployeeTitle() {
    const response = await api.get(baseUrl);
    return response.data;
  }

  public async getEmployeeTitleById(id: any) {
    const response = await api.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateEmployeeTitle(employeeTitle: any) {
    const response = await api.post(baseUrl, employeeTitle);
    return response.data;
  }

  public async updateEmployeeTitle(employeeTitle: any) {
    const response = await api.put(baseUrl, employeeTitle);
    return response.data;
  }

  public async deleteEmployeeTitle(id: any) {
    const response = await api.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
}

export default new EmployeeTitleService();
