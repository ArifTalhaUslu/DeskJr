import { customError } from '../types/customError'

class ErrorHandler{
    private static instance: ErrorHandler;
    private error: customError | null = null

    private constructor() {}

    static getInstance(): ErrorHandler {
        if(!ErrorHandler.instance) {
            ErrorHandler.instance = new ErrorHandler();
        }
        return ErrorHandler.instance;
    }
    setError(error: customError) {
        this.error = error;
    }
    getError(): customError | null {
        return this.error;
    }
    clearError() {
        this.error = null
    }
}

export default ErrorHandler.getInstance();