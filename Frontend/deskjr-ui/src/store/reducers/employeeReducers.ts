import { EmployeeAction, EmployeeState } from "../../types/employee";

const defaultState : EmployeeState ={
    data : [],
    loading: false,
    error : "",
}

const employeeReducer = (state: EmployeeState= defaultState, action: EmployeeAction) =>{
    switch(action.type){
        case "GET_EMPLOYEES_START":
            return {...state,loading:true,error:""}
        case "GET_EMPLOYEES_SUCCESS":
            return {...state,loading: false, data: action.payload}
        case "GET_EMPLOYEES_ERROR":
            return {...state,loading:false, error :"Error Fetching Employees"}
        default: return state;
    }

}

export default employeeReducer;