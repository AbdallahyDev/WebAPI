﻿namespace WebAPI.ViewModels
{
    public class CommentViewModel
    {
        public string Title { get; set; }  
        public int Mark { get; set; }
        public string CommentBody { get; set; } 
        public int UserId { get; set; }
        public int RecetteId { get; set; }  
    }
}
