using Microsoft.AspNetCore.Mvc;

namespace Section7.Models.ModelBinding
{
    public class BookWithFrom
    {
        [FromQuery]
        public int BookID { get; set; }
        [FromRoute]
        public string? Title { get; set; }
        public string? Description { get; set; }        
        public string? Author { get; set; }

    }
}
