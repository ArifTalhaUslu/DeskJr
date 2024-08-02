import { ErrorActionTypes } from "../actions/errorActions";
import { ErrorState } from "../../types/ErrorResponseDto";
import { CLEAR_MESSAGES, SET_ERROR } from "../actions/actionTypes";

const initialState: ErrorState = {
    error: null,
};

const errorReducer = (state = initialState, action: ErrorActionTypes): ErrorState => {
    switch (action.type) {
        case SET_ERROR:
            return {
                ...state,
                error: action.payload,
            };
            case CLEAR_MESSAGES:
                return {
                    ...state,
                    error: null
                };
        default:
            return state;
    }
};

export default errorReducer;