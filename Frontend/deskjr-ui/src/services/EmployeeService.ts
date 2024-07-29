import axios from "axios";

const baseUrl = "https://localhost:7187/api/Employee";

class EmployeeService {
  public async getAllEmployee() {
    const response = await axios.get(baseUrl);
    return response.data;
  }

  public async getEmployeeById(id: any) {
    const response = await axios.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateEmployee(employee: any) {
    const response = await axios.post(baseUrl, employee);
    return response.data;
  }

  public async updateEmployee(employee: any) {
    const response = await axios.put(baseUrl, employee);
    return response.data;
  }

  public async deleteEmployee(id: any) {
    const response = await axios.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
}

export default new EmployeeService();
