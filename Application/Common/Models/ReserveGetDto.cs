namespace Application.Common.Models
{
    public class ReserveGetDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int NumberOfPerson { get; set; }
        public string? MyData { get; set; }
        public string? Time { get; set; }
        public string? Description { get; set; }
    }
}
