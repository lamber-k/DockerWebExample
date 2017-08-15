using System;
using System.ComponentModel.DataAnnotations;

namespace BookModels
{
    public class Book
    {
        [StringLength(17)]
        [Key]
        public string ISBN { get; set; }
        [StringLength(128)]
        [Required]
        public string Title { get; set; }
    }
}
