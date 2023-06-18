namespace Application.Common.Models
{
    public class LunchGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgFileName { get; set; }
        public double Price { get; set; }
        public int Rewievs { get; set; }
        public string Quality { get; set; }
    }
}
