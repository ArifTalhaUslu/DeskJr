namespace DeskJr.Service.Abstract
{
    public interface IUserService
    {
        public string? GetCurrentUserRole();
        public string? GetCurrentUserId();

    }
}
