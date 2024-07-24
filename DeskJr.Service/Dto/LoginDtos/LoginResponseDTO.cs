using DeskJr.Service.Dto.EmployeeDtos;

namespace DeskJr.Service.Dto.LoginDtos
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
