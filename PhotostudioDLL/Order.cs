using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL
{
    public class Order
    {
        public int ID { get; set; }
        [Required]
        public Contract Contract { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Status { get; set; }
    }
}