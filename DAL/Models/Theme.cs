using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public ICollection<Film> Films { get; set; }
    }
}
