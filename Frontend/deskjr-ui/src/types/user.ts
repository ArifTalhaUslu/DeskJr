import { ThunkDispatch } from "redux-thunk";
export interface User {
    id: string;
    name: string;
    dayOfBirth: string;
    employeeRole: number;
    gender: number;
    titleId: string | null;
    teamId: string | null;
    email: string;
    password: string;
}

export interface CurrentSession {
    token: string;
    employee: User;
}

export interface LoginForm {
    email: string;
    password: string;
}

export interface UserState {
    data: User;
    token: string;
    loading: boolean;
    error: string;
}

interface LOGIN_START {
    type: "LOGIN_START";
}

interface LOGIN_SUCCESS {
    type: "LOGIN_SUCCESS";
    payload: CurrentSession;
}

interface LOGIN_ERROR {
    type: "LOGIN_ERROR";
}

export type UserAction = LOGIN_START | LOGIN_SUCCESS | LOGIN_ERROR;
export type UserDispatch = ThunkDispatch<UserState, void, UserAction>;
