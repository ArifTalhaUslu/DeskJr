import { combineReducers } from "redux";
import userReducer from "./userReducer";
import errorReducer from "./errorReducer";

const rootReducer = combineReducers({
    user: userReducer,
    error: errorReducer,
});

export type AppState = ReturnType<typeof rootReducer>;
export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
