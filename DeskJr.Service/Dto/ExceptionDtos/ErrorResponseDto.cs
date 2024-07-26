namespace DeskJr.Service.Dtos
{
    public class ErrorResponseDto
    {
        public int StatusCodes { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
