import axios from "axios";

const baseUrl = "https://localhost:7187/api/Holiday";

class HolidayService {
  public async getAllHoliday() {
    const response = await axios.get(baseUrl);
    return response.data;
  }

  public async getHolidayById(id: any) {
    const response = await axios.get(`${baseUrl}/${id}`);
    return response.data;
  }

  public async addOrUpdateHoliday(holiday: any) {
    const response = await axios.post(baseUrl, holiday);
    return response.data;
  }

  public async updateHoliday(holiday: any) {
    const response = await axios.put(baseUrl, holiday);
    return response.data;
  }

  public async deleteHoliday(id: any) {
    const response = await axios.delete(`${baseUrl}`, {
      data: { id: id },
    });
    return response.data;
  }
}

export default new HolidayService();
