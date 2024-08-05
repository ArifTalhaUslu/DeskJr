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
        const response = await api.post(`${baseUrl}/${id}`);
        return response.data;
    }

    public async getLeavesByEmployeeId(employeeId: any){
        const response = await api.get(`${baseUrl}/leaveByEmployeeId/${employeeId}`);
        return response.data;
    }
}

export default new LeaveService();