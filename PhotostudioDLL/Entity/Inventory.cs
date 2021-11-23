using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity
{
    public class Inventory
    {
        public int ID { get; set; }

        public List<Equipment> Equipment { get; set; } = new List<Equipment>();

        [Required] public string Appointment { get; set; }
    }
}