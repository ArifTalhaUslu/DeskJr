import axios from "axios";

const baseUrl = "https://localhost:7187/api/Team";

class TeamService{
    public async getAllTeam() {
        const response = await axios.get(baseUrl);
        return response.data;
      }
    
      public async getTeamById(id: any) {
        const response = await axios.get(`${baseUrl}/${id}`);
        return response.data;
      }
    
      public async addOrUpdateTeam(team: any) {
        const response = await axios.post(baseUrl, team);
        return response.data;
      }
    
    //   public async updateTeam(id: any) {
    //     const response = await axios.put(`${baseUrl}/${id}`);
    //     return response.data;
    //   }
    
      public async deleteTeam(id: any) {
        const response = await axios.delete(`${baseUrl}/${id}`);
        return response.data;
      }
}

export default new TeamService();