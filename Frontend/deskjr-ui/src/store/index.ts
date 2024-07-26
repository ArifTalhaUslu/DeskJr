import { combineReducers } from "redux";
import { UserState } from "../types/user";
import userReducer from "./reducers/userReducer";
import { EmployeeState } from "../types/employee";
import employeeReducer from "./reducers/employeeReducers";

export interface AppState {
    user: UserState;
    employee : EmployeeState
}

const rootReducer = combineReducers({
    user: userReducer,
    employee : employeeReducer
});

export default rootReducer;
