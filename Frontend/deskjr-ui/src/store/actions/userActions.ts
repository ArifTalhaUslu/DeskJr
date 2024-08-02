import { LoginForm, UserDispatch, User } from "../../types/user";
import api from "../../utils/axiosConfig";
import { showErrorToast } from "../../utils/toastHelper";
import { LOGIN_REQUEST } from "./actionTypes";
import Cookies from 'js-cookie';
export const login = (creds: LoginForm) => async (dispatch: UserDispatch) => {
    dispatch({ type: LOGIN_REQUEST });

    await api
        .post<{
            token: string;
            employee: User;
        }>("/api/login", creds)
        .then((data: any) => {
            const { token, employee } = data.data;
            Cookies.set('token',token, { expires: 7, secure: true, sameSite: 'strict' });
            Cookies.set('id', employee.id ,{ expires: 7, secure: true, sameSite: 'strict' });
            dispatch({ type: "LOGIN_SUCCESS", payload: data.data });
        })
        .catch((err) => {
            showErrorToast(err);
        });
};
