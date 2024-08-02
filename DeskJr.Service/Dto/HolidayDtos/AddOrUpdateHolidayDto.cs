namespace DeskJr.Service.Dto
{
    public class AddOrUpdateHolidayDto
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
