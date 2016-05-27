using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class IngredientViewModel
    {
        public string id { get; set; } 
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        //public virtual ICollection<RecetteIngredient> IngredientRecettes { get; set; }
        public string Picture { get; set; }
        public float Calories { get; set; }
        public string Category { get; set; }
       // public virtual Category Category { get; set; }
    }
}
