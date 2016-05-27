using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class CommentViewModel
    {
        public string Title { get; set; }  
        public int Mark { get; set; }
        public string Comment { get; set; } 
        public int UserId { get; set; } 
    }
}
