using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("reserve")]
    public class Reserve
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("full_name")]
        [Required]
        public string? FullName { get; set; }

        [Column("phone_number")]
        [Required]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Column("number_of_person")]
        [Required]
        public int NumberOfPerson { get; set; }

        [Column("data")]
        [Required]
        public string? MyData { get; set; }

        [Column("time")]
        [Required]
        public string? Time { get; set; }

        [Column("description")]
        [Required]
        public string? Description { get; set; }
    }
}
