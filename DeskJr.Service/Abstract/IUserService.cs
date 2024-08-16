using static DeskJr.Service.Concrete.UserService;
namespace DeskJr.Service.Abstract
{
    public interface IUserService
    {
        CurrentUser GetCurrentUser();
    }
}
