import api from "../utils/axiosConfig";
const baseUrl = "/api/Holiday";

class HolidayService {
  public async getAllHoliday() {
    const response = await api.get(baseUrl);
    return response.data;
  }

  public async getHolidayById(id: any) {
    const response = await api.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateHoliday(holiday: any) {
    const response = await api.post(baseUrl, holiday);
    return response.data;
  }

  public async updateHoliday(holiday: any) {
    const response = await api.put(baseUrl, holiday);
    return response.data;
  }

  public async deleteHoliday(id: any) {
    const response = await api.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
}

export default new HolidayService();
