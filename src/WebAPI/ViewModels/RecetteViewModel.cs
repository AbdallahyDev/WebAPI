using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.ViewModels
{
    public class RecetteViewModel
    {
        public string Id { get; set; } 
        public string Name { get; set; }  
        public int CreatorId { get; set; } 
        public bool IsAvailable { get; set; } 
        public Byte[] Picture { get; set; }   
        public float Calories { get; set; } 
        public ICollection<Ingredient> Ingredients { get; set; } 
        public List<Comment> Comments { get; set; }   
        public string Preparation { get; set; }
        public string Category { get; set; } 
    }
}
