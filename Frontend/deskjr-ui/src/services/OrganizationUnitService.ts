import api from "../utils/axiosConfig";
const baseUrl = "/api/organization";

class OrganizationUnitService {
  public async getAllOrganizationUnits() {
    const response = await api.get(baseUrl);
    return response.data;
  }
}

export default new OrganizationUnitService();