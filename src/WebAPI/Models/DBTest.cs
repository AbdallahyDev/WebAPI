using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace WebAPI.Models
{
    public class DBTest
    {
        //private RecetteRepository _recRepository;
        //private CommunauteRepository _comRepository;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<DBContext>(); 
            var  recRepository = new RecetteRepository(context);

            var communaute = new Communaute() { Bio = "", Birth = 1991, City = "Noukchott", Firstname = "Yarbe", Surname = "cheikh", Level = 3 }; 
            var RecettesIngredient = new HashSet<RecetteIngredient>();
            var category = new Category() { Name="viande"};
            var ing = new Ingredient() {Calories=44.55f };  
            RecetteIngredient recIng1 = new RecetteIngredient() { RecetteId = 2, IngredientId = 1 };    
            //RecetteIngredient recIng2 = new RecetteIngredient() { RecetteId = 2, IngredientId = 2 };
            var recette = new Recette() { Name = "Tajine", Calories = 155.5f, IsAvailable = true, Category="viande", CreatorId = 1, Comments = null, Preparation = "poulet + pomme de terre" }; 
            
           // if (context.Communautes.Any()) 
           // { 
                //NGCookingRepository rep = new NGCookingRepository(context); 
                //rep.Add<Communaute>(ref communaute);
                //IQueryable<Object> results  =rep.GetAll<Communaute>(communaute);  

               /* context.Communautes.Add(communaute); 
                context.Categories.Add(category); 
                context.Ingredients.Add(ing);
                //context.Ingredients.Add(ing); 
                //comRepository.AddCommunaute(communaute); 
                context.Recettes.Add(recette);
                context.RecetteIngredient.Add(recIng1);
                //context.RecetteIngredient.Add(recIng2); 
                context.SaveChanges();  
                /*context.Recettes.AddRange(new List<Recette>() 
                  {
                        new Recette() { Name = "Tajine", Calories=155.5f, IsAvailable =true,Category=new Category(), CreatorId=1, Comments=null,Ingredients=new List<Ingredient>(),Preparation="poulet + pomme de terre"  },
                    });
                context.SaveChanges();  */
           // }
        } 
    }
}
