using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Byte[] Picture { get; set; } 
        public float Calories { get; set; } 
        public string Preparation { get; set; }
        public virtual List<Comment> Comments { get; set; } 
        public string Category { get; set; }    
        public int CreatorId { get; set; }   
    }
}
