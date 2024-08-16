using System.Security.Claims;
using DeskJr.Entity.Types;
using DeskJr.Service.Abstract;
using Microsoft.AspNetCore.Http;

namespace DeskJr.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public class CurrentUser
        {
            public Guid UserId { get; set; }
            public EnumRole Role { get; set; }
        }

        public CurrentUser GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim) ||
                !Guid.TryParse(userIdClaim, out var userId) ||
                !Enum.TryParse(roleClaim, out EnumRole role))
            {
                throw new UnauthorizedAccessException("Invalid Role or UserIdentification Number. Autherization failed.");
            }

            return new CurrentUser
            {
                UserId = userId,
                Role = role
            };
        }
    }
}
