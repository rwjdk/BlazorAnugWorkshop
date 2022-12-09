using System.ComponentModel.DataAnnotations;

namespace SharedModels.Model
{
    public class Bookmark
    {
        public string Guid { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [RegularExpression(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)", ErrorMessage = "Not a valid URL")]
        public string Url { get; set; }
        public string Description { get; set; }
    }
}