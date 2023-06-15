namespace Application.Common.Models
{
    public class BreakFastGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Uri Img { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }
}
