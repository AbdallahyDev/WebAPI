using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class RecetteViewModel
    {
        public string Id { get; set; } 
        public string Name { get; set; }  
        public int CreatorId { get; set; } 
        public bool IsAvailable { get; set; } 
        public string Picture { get; set; }  
        public float Calories { get; set; } 
        public ICollection<string> Ingredients { get; set; } 
        public string Preparation { get; set; } 
        public  ICollection<CommentViewModel> Comments { get; set; }  
    }
}
