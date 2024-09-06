import api from "../utils/axiosConfig";
const baseUrl = "/api/Setting";

class AdvancedSettingService {
  public async getAllSetting() {
    const response = await api.get(baseUrl);
    return response.data;
  }

  public async getSettingById(id: any) {
    const response = await api.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateSetting(setting: any[]) {
    const response = await api.post(baseUrl, setting);
    return response.data;
  }

  public async updateSetting(setting: any) {
    const response = await api.put(baseUrl, setting);
    return response.data;
  }

  public async deleteSetting(id: any) {
    const response = await api.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }

  public async updateMultipleSettings(setting: any[]) {
    const response = await api.post(`${baseUrl}/update-multiple`, setting);
    return response.data;
  }
}

export default new AdvancedSettingService();
