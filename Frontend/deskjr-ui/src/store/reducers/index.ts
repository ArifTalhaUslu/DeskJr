import { combineReducers } from "redux";
import userReducer from "./userReducer";
import employeeReducer from "./employeeReducers";
import errorReducer from "./errorReducer";

const rootReducer = combineReducers({
    user: userReducer,
    employee : employeeReducer,
    error: errorReducer,
});

export type AppState = ReturnType<typeof rootReducer>;
export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
