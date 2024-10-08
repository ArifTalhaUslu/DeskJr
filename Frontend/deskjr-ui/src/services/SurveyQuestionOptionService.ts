import { SET_ERROR } from "../store/actions/actionTypes";
import { SetErrorAction } from "../store/actions/errorActions";
import { ErrorResponseDto } from "../types/ErrorResponseDto";
import api from "../utils/axiosConfig";

const baseUrl = "/api/SurveyQuestionOptions"

export const setError = (error: ErrorResponseDto): SetErrorAction => ({
    type: SET_ERROR,
    payload: error,
});

class SurveyQuestionOptionService {
    public async getAllSurveyQuestionOptions() {
        const response = await api.get(baseUrl);
        return response.data;
    }

    public async addSurveyQuestionOptions(surveyQuestionOption: any) {
        const response = await api.post(baseUrl, surveyQuestionOption);
        return response.data;
    }

    public async updateSurveyQuestionOptions(surveyQuestionOption: any) {
        const response = await api.put(baseUrl, surveyQuestionOption);
        return response.data;
    }

    public async deleteSurveyQuestionOptions(id: any) {
        const response = await api.delete(`${baseUrl}/${id}`);
        return response.data;
    }

    public async getSurveyQuestionOptionsBySurveyQuestionId(surveyQuestionId: any) {
        const response = await api.get(
            `${baseUrl}/GetSurveyQuestionOptionsBySurveyQuestion/${surveyQuestionId}`
        );
        return response.data;
    }

    public async getSurveyQuestionsOptionsById(id: any) {
        const response = await api.get(
            `${baseUrl}/GetSurveyQuestionsOptionsById/${id}`
        );
        return response.data;
    }
}
export default new SurveyQuestionOptionService();