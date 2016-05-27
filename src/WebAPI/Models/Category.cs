using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }   
    }
}