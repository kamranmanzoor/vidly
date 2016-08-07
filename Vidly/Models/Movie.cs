
using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Duration { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "The Release Date field is required")]
        public DateTime ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        public byte NumberInStock { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "The Genre field is required")]
        public byte GenreId { get; set; }


        public byte NumberAvailable { get; set; }


    }
}