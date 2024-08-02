import axios from 'axios';
import { ErrorResponseDto } from '../types/ErrorResponseDto';
import store from '../store/store'
import { setError } from '../store/actions/errorActions';
import { showErrorToast } from './toastHelper';
import Cookies from 'js-cookie';

const api = axios.create({
    baseURL: 'https://localhost:7187',
    timeout: 10000,
});

api.interceptors.request.use(
    (config) => {
        const token = Cookies.get('token');
        if(token){
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

api.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        console.log(error.response);

        let customError: ErrorResponseDto = {
            StatusCodes: 0,
            Message: '',
            Details: '',
        }; 
        if (error.response) {
            const { status, data } = error.response;
            console.log(data)

            customError.StatusCodes = status;
            customError.Message = data.Message;
            customError.Details = data.Details;
        }
    //      else if (error.request){
    //      customError.Message = 'No response from server';
    //        customError.Details = error.message;
    //    }
        else{
            customError.Message = error.message;
            customError.Details = error.details;
        }

        store.dispatch(setError(customError));
        showErrorToast(customError.Message + '. Details: ' + customError.Details);

        return Promise.reject(customError);
    }
);

export default api;