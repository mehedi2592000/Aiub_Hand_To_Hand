﻿using System.ComponentModel.DataAnnotations;

namespace Aiub_Hand_To_Hand_MVC.Models.DataModel
{
    public class RepositoryModel
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
        [Required]
        public int? Count { get; set; }
        [Required]
        public string? Personal { get; set; }

    }
}
