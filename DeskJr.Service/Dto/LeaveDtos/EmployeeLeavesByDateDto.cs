namespace DeskJr.Service.Dto
{
    public class EmployeeLeavesByDateDto
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float DeservedDay { get; set; }
        public float DayUsed { get; set; }
        public float TransferDay { get; set; }
    }
}
