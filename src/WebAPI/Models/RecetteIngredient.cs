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
