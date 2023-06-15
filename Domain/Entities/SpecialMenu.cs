using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("special_menus")]
    public class SpecialMenu : MealBase
    {
        [Required]
        public string Type { get; set; }
    }
}
