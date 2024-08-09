namespace DeskJr.Service.Dto
{
    public class ChangePasswordRequestDTO
    {
        public Guid ID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
