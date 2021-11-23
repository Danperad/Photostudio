using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace PhotostudioDLL
{
    public class Hall
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerHour { get; set; }
    }
}