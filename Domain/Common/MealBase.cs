using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MealBase
{
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("name")]

    public string Name { get; set; }

    [Required]
    [Column("img")]
    public string ImgFileName { get; set; }

    [Required]
    [Column("price")]
    public double Price { get; set; }

    [Required]
    [Column("rewievs")]
    public int Rewievs { get; set; }

    [Required]
    [Column("quality")]
    public string Quality { get; set; }
}
