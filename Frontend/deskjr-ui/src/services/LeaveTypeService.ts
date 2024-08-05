import api from "../utils/axiosConfig";

const baseUrl = "/api/LeaveType";

class LeaveTypeService {
  public async getAllLeaveType() {
    const response = await api.get(baseUrl);
    return response.data;
  }

  public async getLeaveTypeById(id: any) {
    const response = await api.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateLeaveType(leaveType: any) {
    const response = await api.post(baseUrl, leaveType);
    return response.data;
  }

  public async updateLeaveType(leaveType: any) {
    const response = await api.put(baseUrl, leaveType);
    return response.data;
  }

  public async deleteLeaveType(id: any) {
    const response = await api.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
}

export default new LeaveTypeService();
