import { ThunkDispatch } from "redux-thunk";

export interface EmployeeState {
    data: Employee[];
    loading: boolean;
    error: string;
}

export interface Employee {
    id: string;
    name: string;
    dayOfBirth: string;
    employeeRole: number;
    gender: number;
    titleId: any;
    teamId: any;
    email: string;
    password: string;
}

interface GET_START {
    type: "GET_EMPLOYEES_START";
}

interface GET_SUCCESS {
    type: "GET_EMPLOYEES_SUCCESS";
    payload: Employee[];
}

interface GET_ERROR {
    type: "GET_EMPLOYEES_ERROR";
}

export type EmployeeAction = GET_START | GET_SUCCESS |GET_ERROR;
export type EmployeeDispatch = ThunkDispatch<EmployeeState, void,EmployeeAction>;
