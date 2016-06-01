using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace WebAPI.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Recette> Recettes { get; set; } 
        public DbSet<Communaute> Communautes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }  
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecetteIngredient> RecetteIngredient { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*foreach (var entity in modelBuilder.Model.GetEntityTypes())  
            {
                modelBuilder.Entity(entity.Name).ToTable(entity.Name + "s");       
            }*/
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RecetteIngredient>().HasKey(x => new { x.IngredientId, x.RecetteId });
            modelBuilder.Entity<RecetteIngredient>()
                .HasOne(x => x.Recette)
                .WithMany(x => x.RecettesIngredient)
                .HasForeignKey(x => x.RecetteId);
            //.OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RecetteIngredient>()
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.IngredientRecettes)
                .HasForeignKey(x => x.IngredientId);  
        }
    }
}
