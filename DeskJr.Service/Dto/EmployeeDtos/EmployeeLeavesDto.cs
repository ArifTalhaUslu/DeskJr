namespace DeskJr.Service.Dto.EmployeeDtos
{
    public class EmployeeLeavesDto
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MyProperty { get; set; }
    }
}
