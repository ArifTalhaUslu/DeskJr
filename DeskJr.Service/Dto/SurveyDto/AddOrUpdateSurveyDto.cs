namespace DeskJr.Service.Dto
{
    public class AddOrUpdateSurveyDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
    }
}
