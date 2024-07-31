import api from "../utils/axiosConfig";

const baseUrl = "/api/Team";

class TeamService{
    public async getAllTeam() {
        const response = await api.get(baseUrl);
        return response.data;
      }
    
      public async getTeamById(id: any) {
        const response = await api.get(`${baseUrl}/${id}`);
        return response.data;
      }
    
      public async addOrUpdateTeam(team: any) {
        const response = await api.post(baseUrl, team);
        return response.data;
      }
    
    //   public async updateTeam(id: any) {
    //     const response = await api.put(`${baseUrl}/${id}`);
    //     return response.data;
    //   }
    
      public async deleteTeam(id: any) {
        const response = await api.delete(`${baseUrl}/${id}`);
        return response.data;
      }
}

export default new TeamService();