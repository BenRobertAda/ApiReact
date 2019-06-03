using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titre { get; set; }
        public string Description { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string Vignette { get; set; } = "http://localhost:59233/Images/imagevignette.png";

        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}
