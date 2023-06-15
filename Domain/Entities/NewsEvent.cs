using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("news_event")]
    public class NewsEvent
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("img")]
        public Uri Img { get; set; }

        [Column("time")]
        public string Time { get; set; }

        [Column("priority")]
        public string Priority { get; set; }
    }
}
