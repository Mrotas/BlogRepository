using System.ComponentModel.DataAnnotations;

namespace BlogRepository.Models.Blog
{
    public class BlogEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
