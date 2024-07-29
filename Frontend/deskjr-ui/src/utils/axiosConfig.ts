import axios from 'axios';
import { customError } from '../types/customError';
import ErrorHandler from './ErrorHandler';

const api = axios.create({
    baseURL: 'https://localhost:7187',
    timeout: 10000,
    withCredentials: true,
});

api.interceptors.request.use(
    (config) => {
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
        let customError: customError = {
            status: 0,
            message: 'error',
            details: '',
        }; 
        if(error.response){
            const {status, data} = error.response;
            customError = {
                status: status,
                message: data.message,
                details: data.details
            };
        }
        else if (error.request){
            customError.message = 'Network error ELSE IF';
            customError.details = error.message;
        }
        else{
            customError.message = error.message;
            customError.details = '';
        }

        ErrorHandler.setError(customError);
        return Promise.reject(customError);
    }
);

export default api;