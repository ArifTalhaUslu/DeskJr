import { SET_ERROR } from "../store/actions/actionTypes";
import { SetErrorAction } from "../store/actions/errorActions";
import { ErrorResponseDto } from "../types/ErrorResponseDto";
import api from "../utils/axiosConfig";

const baseUrl = "/api/SurveyQuestion"

export const setError = (error: ErrorResponseDto): SetErrorAction => ({
    type: SET_ERROR,
    payload: error,
});

class SurveyQuestionService {
    public async getAllSurveyQuestion() {
        const response = await api.get(baseUrl);
        return response.data;
    }

    public async addOrUpdateSurveyQuestion(surveyQuestion: any) {
        const response = await api.post(baseUrl, surveyQuestion);
        return response.data;
    }

    public async deleteSurveyQuestion(id: any) {
        const response = await api.delete(`${baseUrl}/${id}`);
        return response.data;
    }

    public async getSurveyQuestionById(id: any) {
        const response = await api.get(`${baseUrl}/${id}`);
        return response.data;
    }

    public async getSurveyQuestionsBySurveyId(surveyId: any) {
        const response = await api.get(
            `${baseUrl}/GetSurveyQuestionsBySurveyId/${surveyId}`
        );
        return response.data;
    }
}
export default new SurveyQuestionService();