import { User, UserAction, UserState } from "../../types/user";
import { LOGIN_REQUEST, LOGIN_SUCCESS, LOGIN_FAILURE } from "../actions/actionTypes";

const defaultState: UserState = {
    data: {} as User,
    token: null,
    loading: false,
    error: null,
};

const userReducer = (state: UserState = defaultState, action: UserAction) => {
    switch (action.type) {
        case LOGIN_REQUEST:
            return { 
                ...state, 
                loading: true, 
                error: null 
            };
        case LOGIN_SUCCESS:
            return {
                ...state,
                loading: false,
                data: action.payload.employee,
                token: action.payload.token,
            };
        case LOGIN_FAILURE:
            return { ...state, loading: false, error: action.payload };
        default:
            return state;
    }
};

export default userReducer;
