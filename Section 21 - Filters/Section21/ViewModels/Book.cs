using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Section21.ViewModels
{
    public class Book
    {
        [Required]
        public int BookID { get; set; }
        public string? Title { get; set; }
        [MaxLength(100),MinLength(20)]
        public string? Description { get; set; }
        public string? Author { get; set; }
        [Range(200,1500)]
        public double? price { get; set; }

        public override string ToString()
        {
            return $"{BookID} | {Title} | {Description} | {Author} | {price}";
        }
    }
}
