import { ThunkDispatch } from "redux-thunk";
import { AnyAction } from "redux";
export interface User {
    id: string;
    name: string;
    dayOfBirth: string;
    employeeRole: number;
    gender: number;
    employeeTitleId: string | null;
    teamId: string | null;
    email: string;
    password: string;
    team: any;
    employeeTitle: any;
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
    data: User | null;
    token: string | null;
    loading: boolean;
    error: string | null;
}

interface LOGIN_REQUEST {
    type: "LOGIN_REQUEST";
}

interface LOGIN_SUCCESS {
    type: "LOGIN_SUCCESS";
    payload: CurrentSession;
}

interface LOGIN_FAILURE {
    type: "LOGIN_FAILURE";
    payload: string;
}

export type UserAction = LOGIN_REQUEST | LOGIN_SUCCESS | LOGIN_FAILURE;
export type UserDispatch = ThunkDispatch<UserState, void, AnyAction>;
