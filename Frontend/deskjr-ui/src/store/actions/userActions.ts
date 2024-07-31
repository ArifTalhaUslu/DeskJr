import { LoginForm, UserDispatch, User } from "../../types/user";
import api from "../../utils/axiosConfig";
import { showErrorToast } from "../../utils/toastHelper";
import { LOGIN_REQUEST } from "./actionTypes";

export const login = (creds: LoginForm) => async (dispatch: UserDispatch) => {
    dispatch({ type: LOGIN_REQUEST });

    await api
        .post<{
            token: string;
            employee: User;
        }>("/api/login", creds)
        .then((data: any) => {
            const { token, employee } = data.data;
            localStorage.setItem("token", token);
            localStorage.setItem("id", employee.id);
            dispatch({ type: "LOGIN_SUCCESS", payload: data.data });
        })
        .catch((err) => {
          showErrorToast(err);
        });
};
