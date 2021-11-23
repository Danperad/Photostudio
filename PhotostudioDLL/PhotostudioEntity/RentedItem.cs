using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotostudioDLL.Entity
{
    public class RentedItem
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
    }
}