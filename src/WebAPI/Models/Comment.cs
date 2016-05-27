using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Comment
    {
        public int Id { get; set; } 
        public string Title { get; set;}
        public int Mark { get; set; } 
        public string CommentBody { get; set;}
        public int RecetteId { get; set; }
        [ForeignKey("RecetteId")]
        public virtual Recette Recette { get; set;}
        public int UserId { get; set; } 
        [ForeignKey("UserId")]
        public virtual Communaute Communaute { get; set;}     
    }
}