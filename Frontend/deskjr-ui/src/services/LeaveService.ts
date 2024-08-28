import { SET_ERROR } from "../store/actions/actionTypes";
import { SetErrorAction } from "../store/actions/errorActions";
import { ErrorResponseDto } from "../types/ErrorResponseDto";
import api from "../utils/axiosConfig";

const baseUrl = '/api/Leave';

export const setError = (error: ErrorResponseDto): SetErrorAction => ({
    type: SET_ERROR,
    payload: error,
  });

class LeaveService {
    public async getAllLeaves(){
        const response = await api.get(baseUrl);
        return response.data;
    }

    public async getLeaveById(id:any){
        const response = await api.get(`${baseUrl}/${id}`);
        return response.data;
    }

    public async createLeave(leave: any){
        const response = await api.post(baseUrl,leave);
        return response.data;
    }

    public async updateLeave(leave: any){
        const response = await api.put(baseUrl,leave);
        return response.data;
    }

    public async deleteLeave(id: any){
        const response = await api.delete(`${baseUrl}/${id}`);
        return response.data;
    }

    public async getLeavesByEmployeeId(){
        const response = await api.get(`${baseUrl}/leaveByEmployeeId/`);
        return response.data;
    }

    public async getPendingLeavesForApproverEmployeeByEmployeeId(){
        const response = await api.post(`${baseUrl}/pendingLeaves`, );
        return response.data;
    }

    public async updateLeaveStatus(leaveId: any, newStatus: any, confirmDescription?: string){
        const response = await api.post(`${baseUrl}/updateStatus`, {leaveId, newStatus, confirmDescription});
        return response.data;
    }

    public async getRecentValidLeaves() {
        const response = await api.get(`${baseUrl}/recentValidLeaves`);
        return response.data;
    }
    public async getAllLeavesByManagerId(){
        const response = await api.get(`${baseUrl}/leaveApproval/`);
        return response.data;
    }
}

export default new LeaveService();