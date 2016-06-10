using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} est obligatoire.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")]
        public string Title { get; set;}
        [Required(ErrorMessage = "{0} est obligatoire.")]
        public int Mark { get; set; }
        [Required(ErrorMessage = "{0} est obligatoire.")]
        [StringLength(255, MinimumLength = 20, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")]
        public string CommentBody { get; set;} 
        public virtual Recette Recette { get; set;}
        public int UserId { get; set; }     
    }
}