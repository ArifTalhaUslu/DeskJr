import { ErrorResponseDto } from "../../types/ErrorResponseDto";
import { SET_ERROR, CLEAR_MESSAGES } from "./actionTypes";

export interface SetErrorAction {
    type: typeof SET_ERROR;
    payload: ErrorResponseDto;
}

export interface ClearMessagesActions {
    type: typeof CLEAR_MESSAGES;
}

export const setError = (error: ErrorResponseDto): SetErrorAction => ({
    type: SET_ERROR,
    payload: error,
});

export const clearMessages = (): ClearMessagesActions => ({
    type: CLEAR_MESSAGES,
});

export type ErrorActionTypes = SetErrorAction | ClearMessagesActions;

