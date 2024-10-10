import api from "../utils/axiosConfig";

const baseUrl = "/api/Log"

class LogService {
    public async getLogs() {
        const response = await api.get(`${baseUrl}`);
        return response.data;
    }
}
export default new LogService();