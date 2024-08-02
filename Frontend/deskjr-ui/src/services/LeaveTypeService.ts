import axios from "axios";

const baseUrl = "https://localhost:7187/api/LeaveType";

class LeaveTypeService {
  public async getAllLeaveType() {
    const response = await axios.get(baseUrl);
    return response.data;
  }

  public async getLeaveTypeById(id: any) {
    const response = await axios.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateLeaveType(leaveType: any) {
    const response = await axios.post(baseUrl, leaveType);
    return response.data;
  }

  public async updateLeaveType(leaveType: any) {
    const response = await axios.put(baseUrl, leaveType);
    return response.data;
  }

  public async deleteLeaveType(id: any) {
    const response = await axios.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
}

export default new LeaveTypeService();
