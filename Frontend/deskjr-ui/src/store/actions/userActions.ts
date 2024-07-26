import { LoginForm, UserDispatch, User } from "../../types/user";
import api from "../../utils/api";

export const login = (creds: LoginForm) => async (dispatch: UserDispatch) => {
    dispatch({ type: "LOGIN_START" });

    await api
        .post<{
            token: string;
            employee: User;
        }>("/api/login", creds)
        .then((data: any) => {
            const { token, employee } = data.data;
            localStorage.setItem("token", token);
            localStorage.setItem("employee", JSON.stringify(employee));
            dispatch({ type: "LOGIN_SUCCESS", payload: data.data });
        })
        .catch((err: any) => {
            dispatch({ type: "LOGIN_ERROR" });
        }); 
};
