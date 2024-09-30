import api from "../utils/axiosConfig";

const baseUrl = "/api/EmployeeOptions"

class EmployeeOptionsService {
    public async getAllEmployeeOptions() {
        const response = await api.get(`${baseUrl}/GetAllEmployeeOptions`);
        return response.data;
    }

    public async addOrUpdateEmployeeOptions(employeeOptions: any) {
        const response = await api.post(`${baseUrl}/AddOrUpdateEmployeeOptions`, employeeOptions);
        return response.data;
    }

    public async addRangeEmployeeOptions(payload: any) {
        const response = await api.post(`${baseUrl}/AddRange`, payload);
        return response.data;
    }

    public async deleteEmployeeOptions(id: any) {
        const response = await api.delete(`${baseUrl}/DeleteEmployeeOptions/${id}`);
        return response.data;
    }

    public async getEmployeeOptionsById(id: any) {
        const response = await api.get(`${baseUrl}/GetEmployeeOptionsById/${id}`);
        return response.data;
    }

    public async getEmployeeSurveyStatus(userId: any, surveyId: any) {
        const response = await api.get(`${baseUrl}/EmployeeSurveyStatus/${userId}/${surveyId}`);
        return response.data;
    }

}
export default new EmployeeOptionsService();