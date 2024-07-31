import React, { useEffect } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../../store/reducers";
import { showErrorToast } from "../../utils/toastHelper";

const ErrorComponent: React.FC = () => {
    const error = useSelector((state: RootState) => state.error.error);

    useEffect(() => {
        if (error) {
            showErrorToast(error.Message)
        }
    }, [error]); 
    return null;
};

export default ErrorComponent;