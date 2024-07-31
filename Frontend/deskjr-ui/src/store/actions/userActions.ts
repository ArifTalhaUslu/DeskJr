import { LoginForm, UserDispatch, User } from "../../types/user";
import api from '../../utils/axiosConfig';
import { setError } from "./errorActions";
import { LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE } from "./actionTypes";

export const login = (creds: LoginForm) => async (dispatch: UserDispatch) => {
    dispatch({ type: LOGIN_REQUEST });

    try {
        const { data } = await api.post<{ token: string; employee: User; }>("/api/login", creds);
        const { token, employee } = data;
        localStorage.setItem("token", token);
        localStorage.setItem("employee", JSON.stringify(employee));
        dispatch({ type: LOGIN_SUCCESS, payload: {token, employee} });
    } catch (error: any) {
        dispatch({ type: LOGIN_FAILURE, payload: error.message});
        dispatch(setError(error));
    } 
};
