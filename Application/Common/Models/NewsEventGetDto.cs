namespace Application.Common.Models
{
    public class NewsEventGetDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Uri Img { get; set; }
        public string Time { get; set; }
        public string Priority { get; set; }
    }
}
