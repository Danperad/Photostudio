using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL
{
    public class Inventory
    {
        public int ID { get; set; }
        // public Service Service { get; set; }
        public List<Equipment> Equipment { get; set; } = new List<Equipment>();
        [Required]
        public string Appointment { get; set; }
    }
}