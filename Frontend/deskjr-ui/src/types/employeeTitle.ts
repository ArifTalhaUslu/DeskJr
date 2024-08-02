import { ThunkDispatch } from "redux-thunk";

export interface EmployeeTitleState {
  data: EmployeeTitle[];
  loading: boolean;
  error: string;
}

export interface EmployeeTitle {
  id: string;
  employeeTitle: string;
}

interface GET_START {
  type: "GET_EMPLOYEES_START";
}

interface GET_SUCCESS {
  type: "GET_EMPLOYEES_SUCCESS";
  payload: EmployeeTitle[];
}

interface GET_ERROR {
  type: "GET_EMPLOYEES_ERROR";
}

export type EmployeeTitleAction = GET_START | GET_SUCCESS | GET_ERROR;
export type EmployeeTitleDispatch = ThunkDispatch<
  EmployeeTitleState,
  void,
  EmployeeTitleAction
>;
