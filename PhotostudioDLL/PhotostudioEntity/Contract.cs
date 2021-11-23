using System;
using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entity
{
    public class Contract
    {
        public int ID { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Employee Employee { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}