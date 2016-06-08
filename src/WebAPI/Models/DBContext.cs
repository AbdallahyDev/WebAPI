using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAPI.Models
{
    public class DBContext : IdentityDbContext<Communaute> 
    {
        public DbSet<Recette> Recettes { get; set; }  
       // public DbSet<Communaute> Communautes { get; set; }
        public DbSet<Comment> Comments { get; set; }   
        public DbSet<Ingredient> Ingredients { get; set; }  
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecetteIngredient> RecetteIngredient { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<RecetteIngredient>().HasKey(x => new { x.IngredientId, x.RecetteId });  
            modelBuilder.Entity<RecetteIngredient>()
                .HasOne(x => x.Recette)
                .WithMany(x => x.RecettesIngredient)
                .HasForeignKey(x => x.RecetteId); 
            modelBuilder.Entity<RecetteIngredient>()
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.IngredientRecettes) 
                .HasForeignKey(x => x.IngredientId);   
        }
    }
}
