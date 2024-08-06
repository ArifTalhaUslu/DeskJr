namespace DeskJr.Service.Dto
{
    public class ChangePasswordRequestDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
