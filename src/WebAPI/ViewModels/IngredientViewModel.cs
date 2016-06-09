namespace WebAPI.ViewModels
{
    public class IngredientViewModel
    {
        public string id { get; set; } 
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string Picture { get; set; }
        public float Calories { get; set; }
        public string Category { get; set; }  
    }
}
