import { combineReducers } from "redux";
import { UserState } from "../types/user";
import userReducer from "./reducers/userReducer";
import { EmployeeState } from "../types/employee";

export interface AppState {
    user: UserState;
    employee : EmployeeState
}

const rootReducer = combineReducers({
    user: userReducer
});

export default rootReducer;
