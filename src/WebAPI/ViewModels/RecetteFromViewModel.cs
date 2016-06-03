using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.ViewModels
{
    public class RecetteFromViewModel
    {
        public string Name { get; set; }
        public int CreatorId { get; set; }
       // public bool IsAvailable { get; set; }
        public string Picture { get; set; }
        public float Calories { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }     
        public string Prepa { get; set; }
        public Category Category { get; set; }  
    }
}
