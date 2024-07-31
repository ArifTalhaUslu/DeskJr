import { Employee, EmployeeDispatch } from "../../types/employee";
import api from "../../utils/axiosConfig";
import { setError } from "./errorActions";
import { ErrorResponseDto } from "../../types/ErrorResponseDto";
import { LOGIN_FAILURE, LOGIN_REQUEST, LOGIN_SUCCESS } from "./actionTypes";

export const GetEmployees = () => async (dispatch : EmployeeDispatch) =>{
    dispatch ({type: LOGIN_REQUEST});
    try{
        const response = await api.get<Employee[]>("/api/employee");
        dispatch({type: LOGIN_SUCCESS, payload:response.data});
    }catch (error: any){
        const customError: ErrorResponseDto = {
            StatusCodes: error.response?.status || 500,
            Message: error.message,
            Details: error.response.data || '',
        };
        dispatch({type: LOGIN_FAILURE, payload: error.message});
        dispatch(setError(customError));
    }
};

