using System.ComponentModel.DataAnnotations;


namespace Aiub_Hand_To_Hand_MVC.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Gmail { get; set; }
        [Required]
        public string?  Instutue { get; set; }
        [Required]
        public string?  Username { get; set; }
        [Required]
        public string? Password { get; set; }
       
        public string? Picture { get; set; }
        

        public virtual ICollection<Repository> Repositories { get; set; }
    }
}
