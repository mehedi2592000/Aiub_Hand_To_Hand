using System.ComponentModel.DataAnnotations;

namespace Aiub_Hand_To_Hand_MVC.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Depertment { get; set; }

        [Required]
        public string Jobtitle { get; set; }

        [Required]
        public string Message { get; set; }

        
        public DateTime? DateNow { get; set; }

        public DateTime? Exparied { get; set; }

        public int LoginId { get; set; }

        public virtual Login Login { get; set; }

    }
}
