using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Recette
    {
        public Recette()
        {
            RecettesIngredient = new HashSet<RecetteIngredient>();  
        }
        public int Id { get; set; } 
        public ICollection<RecetteIngredient> RecettesIngredient { get; set; }
        [Required(ErrorMessage = "{0} est obligatoire.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")]
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Byte[] Picture { get; set; } 
        public float Calories { get; set; }
        [Required(ErrorMessage = "{0} est obligatoire.")]
        [StringLength(1024, MinimumLength = 20, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")]
        public string Preparation { get; set; }
        public virtual List<Comment> Comments { get; set; }
        [Required(ErrorMessage = "{0} est obligatoire.")]
        public string Category { get; set; }    
        public int CreatorId { get; set; }   
    }
}
