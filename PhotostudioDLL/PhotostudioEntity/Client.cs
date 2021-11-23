using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhotostudioDLL.Entity
{
    public class Client
    {
        public int ID { get; set; }
        [Required] [MaxLength(50)] public string LastName { get; set; }
        [Required] [MaxLength(50)] public string FirstName { get; set; }
        [MaxLength(50)] public string MiddleName { get; set; }
        [Required] [MaxLength(15)] public string PhoneNumber { get; set; }
        [MaxLength(50)] public string EMail { get; set; }
    }
}