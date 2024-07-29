import axios from "axios";

const baseUrl = "https://localhost:7187/api/EmployeeTitle";

class EmployeeTitleService {
  public async getAllEmployeeTitle() {
    const response = await axios.get(baseUrl);
    return response.data;
  }

  public async getEmployeeTitleById(id: any) {
    const response = await axios.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateEmployeeTitle(employee: any) {
    const response = await axios.post(baseUrl, employee);
    return response.data;
  }

  public async updateEmployeeTitle(employee: any) {
    const response = await axios.put(baseUrl, employee);
    return response.data;
  }

  public async deleteEmployeeTitle(id: any) {
    const response = await axios.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
}

export default new EmployeeTitleService();
