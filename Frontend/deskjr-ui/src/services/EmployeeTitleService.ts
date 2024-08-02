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

  public async addOrUpdateEmployeeTitle(employeeTitle: any) {
    const response = await axios.post(baseUrl, employeeTitle);
    return response.data;
  }

  public async updateEmployeeTitle(employeeTitle: any) {
    const response = await axios.put(baseUrl, employeeTitle);
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
