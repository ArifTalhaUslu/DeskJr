import api from "../utils/axiosConfig";

const baseUrl = "/api/Survey"

class SurveyService {
    public async getAllSurvey() {
        const response = await api.get(baseUrl);
        return response.data;
    }

    public async addOrUpdateSurvey(survey: any) {
        const response = await api.post(baseUrl, survey);
        return response.data;
    }

    public async deleteSurvey(id: any) {
        const response = await api.delete(`${baseUrl}/${id}`);
        return response.data;
    }

    public async getSurveyById(id: any) {
        const response = await api.get(`${baseUrl}/${id}`);
        return response.data;
    }
}
export default new SurveyService();