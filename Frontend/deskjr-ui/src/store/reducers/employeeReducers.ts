import { EmployeeState, EmployeeAction } from "../../types/employee";
import { LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE } from "../actions/actionTypes";

const defaultState : EmployeeState ={
    data : [],
    loading: false,
    error : "",
}

const employeeReducer = (state: EmployeeState= defaultState, action: EmployeeAction) =>{
    switch (action.type) {
        case LOGIN_REQUEST:
            return { 
                ...state, 
                loading: true, 
                error: null 
            };
        case LOGIN_SUCCESS:
            return {
                ...state,
                loading: false,
                data: action.payload.employee,
                token: action.payload.token,
            };
        case LOGIN_FAILURE:
            return { ...state, loading: false, error: action.payload };
        default:
            return state;
    }
};

export default employeeReducer;