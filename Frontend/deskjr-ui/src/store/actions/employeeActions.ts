import { Employee, EmployeeDispatch } from "../../types/employee";
import api from "../../utils/api";

export const GetEmployees = () => async (dispatch : EmployeeDispatch) =>{
    dispatch ({type: "GET_EMPLOYEES_START"});
    try{
        const response = await api.get<Employee[]>("/api/employee");
        dispatch({type: "GET_EMPLOYEES_SUCCESS", payload:response.data});
    }catch{
        dispatch({type: "GET_EMPLOYEES_ERROR"});
    }
}

