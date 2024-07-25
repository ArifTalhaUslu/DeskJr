import { LoginForm, UserDispatch, User } from "../../types/user";
import api from "../../utils/api";

export const login = (creds: LoginForm) => async (dispatch: UserDispatch) => {
    dispatch({ type: "LOGIN_START" });
    try {
        const response = await api.post<{
            token: string;
            employee: User;
        }>("/api/login", creds);

        const { token, employee } = response.data;
        localStorage.setItem("token", token);
        localStorage.setItem("employee", JSON.stringify(employee));
        dispatch({ type: "LOGIN_SUCCESS", payload: response.data });
        
    } catch {
        dispatch({ type: "LOGIN_ERROR" });
    }
};
