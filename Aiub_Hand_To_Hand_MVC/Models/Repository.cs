using System.ComponentModel.DataAnnotations;

namespace Aiub_Hand_To_Hand_MVC.Models
{
    public class Repository
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Pdf { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        
        public int? Count { get; set; }
        
        public string? Personal { get; set; }

        public int LoginId { get; set; }

        public virtual Login Login { get; set; }
    }
}
