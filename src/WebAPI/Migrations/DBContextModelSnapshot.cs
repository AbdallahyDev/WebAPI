using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using WebAPI.Models;

namespace WebAPI.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebAPI.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentBody");

                    b.Property<int>("Mark");

                    b.Property<int?>("RecetteId");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebAPI.Models.Communaute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio");

                    b.Property<int>("Birth");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<int>("Level");

                    b.Property<string>("Password");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("Surname");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebAPI.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Calories");

                    b.Property<string>("Category");

                    b.Property<bool>("IsAvailable");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Picture");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebAPI.Models.Recette", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Calories");

                    b.Property<string>("Category");

                    b.Property<int>("CreatorId");

                    b.Property<bool>("IsAvailable");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("Preparation");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebAPI.Models.RecetteIngredient", b =>
                {
                    b.Property<int>("IngredientId");

                    b.Property<int>("RecetteId");

                    b.HasKey("IngredientId", "RecetteId");
                });

            modelBuilder.Entity("WebAPI.Models.Comment", b =>
                {
                    b.HasOne("WebAPI.Models.Recette")
                        .WithMany()
                        .HasForeignKey("RecetteId");

                    b.HasOne("WebAPI.Models.Communaute")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebAPI.Models.Recette", b =>
                {
                    b.HasOne("WebAPI.Models.Communaute")
                        .WithMany()
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("WebAPI.Models.RecetteIngredient", b =>
                {
                    b.HasOne("WebAPI.Models.Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId");

                    b.HasOne("WebAPI.Models.Recette")
                        .WithMany()
                        .HasForeignKey("RecetteId");
                });
        }
    }
}
