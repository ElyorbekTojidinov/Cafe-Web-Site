namespace Application.Common.Models;
public class SpetialMenuGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Uri Img { get; set; }
    public double Price { get; set; }
    public int Rewievs { get; set; }
    public string Quality { get; set; }
}
