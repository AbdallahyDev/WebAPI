using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class RecetteIngredient
    {
        public int RecetteId { get; set; }
        
        public Recette Recette { get; set; } 
       
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }  
    }
}
