using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace WebAPI.Migrations
{
    public partial class NGDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Communaute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bio = table.Column<string>(nullable: true),
                    Birth = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Picture = table.Column<byte[]>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communaute", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calories = table.Column<float>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Recette",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calories = table.Column<float>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<byte[]>(nullable: true),
                    Preparation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recette", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recette_Communaute_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Communaute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentBody = table.Column<string>(nullable: true),
                    Mark = table.Column<int>(nullable: false),
                    RecetteId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Recette_RecetteId",
                        column: x => x.RecetteId,
                        principalTable: "Recette",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Communaute_UserId",
                        column: x => x.UserId,
                        principalTable: "Communaute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "RecetteIngredient",
                columns: table => new
                {
                    IngredientId = table.Column<int>(nullable: false),
                    RecetteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetteIngredient", x => new { x.IngredientId, x.RecetteId });
                    table.ForeignKey(
                        name: "FK_RecetteIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetteIngredient_Recette_RecetteId",
                        column: x => x.RecetteId,
                        principalTable: "Recette",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Category");
            migrationBuilder.DropTable("Comment");
            migrationBuilder.DropTable("RecetteIngredient");
            migrationBuilder.DropTable("Ingredient");
            migrationBuilder.DropTable("Recette");
            migrationBuilder.DropTable("Communaute");
        }
    }
}
