using DeskJr.Entity.Types;

namespace DeskJr.Service.Dto
{
    public class PendingLeaveRequestDto
    {
        public Guid currentUserId { get; set; }

        public EnumRole role { get; set; }
    }
}
