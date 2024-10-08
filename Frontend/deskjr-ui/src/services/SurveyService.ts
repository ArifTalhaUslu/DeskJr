import api from "../utils/axiosConfig";

const baseUrl = "/api/Survey"

class SurveyService {
    public async getAllSurvey() {
        const response = await api.get(`${baseUrl}/GetAllSurvey`);
        return response.data;
    }

    public async addOrUpdateSurvey(survey: any) {
        const response = await api.post(`${baseUrl}/AddOrUpdateSurvey`, survey);
        return response.data;
    }

    public async deleteSurvey(id: any) {
        const response = await api.delete(`${baseUrl}/DeleteSurvey/${id}`);
        return response.data;
    }

    public async getSurveyById(id: any) {
        const response = await api.get(`${baseUrl}/GetSurveyById/${id}`);
        return response.data;
    }

    public async getSurveyAllElements(id: any) {
        const response = await api.get(`${baseUrl}/GetSurveyAllElements/${id}`, {
            data: { id: id },
        });
        return response.data;
    }
}
export default new SurveyService();