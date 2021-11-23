using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL
{
    public class Role
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Rights { get; set; }
        [Required]
        public string Responsibilities { get; set; }
    }
}