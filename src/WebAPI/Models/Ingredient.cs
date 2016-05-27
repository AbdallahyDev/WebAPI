using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            IngredientRecettes = new HashSet<RecetteIngredient>();
        }
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public bool IsAvailable { get; set; }    
        public virtual ICollection<RecetteIngredient> IngredientRecettes { get; set; }
        public Byte[] Picture { get; set; }
        public float Calories { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } 
    }
}
