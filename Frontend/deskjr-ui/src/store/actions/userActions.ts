import { LoginForm, UserDispatch, User } from "../../types/user";
import api from "../../utils/api";

export const login = (creds: LoginForm) => async (dispatch: UserDispatch) => {
    dispatch({ type: "LOGIN_START" });

    await api
        .post("/api/login", creds)
        .then((response) => {
            dispatch({ type: "LOGIN_SUCCESS", payload: response.data });
        })
        .catch((err) => {
            dispatch({ type: "LOGIN_ERROR" });
        });
};
