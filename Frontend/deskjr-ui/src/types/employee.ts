import { ThunkDispatch } from "redux-thunk";
import { AnyAction } from "redux";

export interface EmployeeState {
    data: Employee[];
    loading: boolean;
    error: string;
}

export interface CurrentSession {
    token: string;
    employee: Employee;
}

export interface Employee {
    id: string;
    name: string;
    dayOfBirth: string;
    employeeRole: number;
    gender: number;
    employeeTitleId: any;
    teamId: any;
    email: string;
    password: string;
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

export type EmployeeAction = LOGIN_REQUEST | LOGIN_SUCCESS | LOGIN_FAILURE;
export type EmployeeDispatch = ThunkDispatch<EmployeeState, void, AnyAction>;
