using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using System.Net;
using WebAPI.ViewModels;
using WebAPI.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RecetteController : Controller
    {
        private INGCookingRepository _ngCookingRepository; 
        private Recette _recette;
        private ILogger<Recette> _logger;

        public RecetteController(INGCookingRepository iNGCookingRep, ILogger<Recette> logger)
        {
            _ngCookingRepository = iNGCookingRep;  
            _recette = new Recette();   
            _logger = logger;  
        }
        // GET: api/values
        [HttpGet]
        public JsonResult Get() 
        {
            List<Object> recettes = (_ngCookingRepository.GetAll<Recette>(_recette)).ToList();
            List<RecetteViewModel> recettesVM = new List<RecetteViewModel>();  
            foreach (Recette recette in recettes)
            {
                List<Ingredient> ingDB = _ngCookingRepository.GetIngredientsByRecetteId(recette.Id).ToList();
                List<Comment> commentsDB = _ngCookingRepository.GetCommentsByRecetteId(recette.Id).ToList();
                RecetteViewModel recetteVM = new RecetteViewModel()
                {
                    Id = recette.Id.ToString(),
                    Calories = recette.Calories, 
                    Category = recette.Category,    
                    CreatorId = recette.CreatorId,  
                    IsAvailable = recette.IsAvailable, 
                    Name = recette.Name,
                    Picture = recette.Picture, 
                    Preparation = recette.Preparation  
                };
                _logger.LogInformation($"ça marhe pour les ingredients:{ingDB.Count}");
                recetteVM.Ingredients = new List<Ingredient>();
                recetteVM.Comments = new List<Comment>(); 
                //formattage de ingredients
                foreach (Ingredient ingredient in ingDB)
                {
                    recetteVM.Ingredients.Add((Ingredient)_ngCookingRepository.FindById(ingredient.Id, "Ingredient"));  
                }
                //formattage de comments
                foreach (Comment comment in commentsDB)
                {
                    Comment cmnt = new Comment() {CommentBody=comment.CommentBody,Id=comment.Id,Mark=comment.Mark,Title=comment.Title,UserId=comment.UserId};
                    recetteVM.Comments.Add(cmnt); 
                }
                recettesVM.Add(recetteVM);   
            }
            _logger.LogInformation($"ça marhe pour les recettesVM:{recettesVM.Count}");    
            return Json(recettesVM); 
            
        }

        // GET api/values/5
        [HttpGet("{id}")] 
        public string Get(int id)
        {
            return "value";  
        }

        [HttpPost]
        public JsonResult Post([FromBody]RecetteFromViewModel recetteVM)  
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newRecette = Mapper.Map<Recette>(recetteVM); 
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    var pic = _ngCookingRepository.FileToByteArray(recetteVM.Picture);
                    RecetteFromViewModel newRec= recetteVM;
                    _logger.LogInformation($"adding successfuly:{recetteVM.Name}");   
                        var newRecette = new Recette()
                        {
                            Calories = recetteVM.Calories, 
                            CreatorId = recetteVM.CreatorId, 
                           // IsAvailable = recetteVM.IsAvailable,
                            Name = recetteVM.Name,
                            Preparation = recetteVM.Prepa,
                            Category = recetteVM.Category.Name,
                            //Picture = _ngCookingRepository.FileToByteArray(recetteVM.Picture)
                        };
                   _ngCookingRepository.Add<Recette>(newRecette);  
                    RecetteIngredient newRecetteIngredient = null;  
                    foreach (var ing in recetteVM.Ingredients) 
                    {
                            object ingredient = _ngCookingRepository.FindByName(ing.Name, "Ingredient");
                            newRecetteIngredient = new RecetteIngredient() { IngredientId = ((Ingredient)ingredient).Id, RecetteId = ((Recette)_ngCookingRepository.FindByName(newRecette.Name,"Recette")).Id};
                            _ngCookingRepository.Add<RecetteIngredient>(newRecetteIngredient);  
                    }
                    return Json("it is succesfully added");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError("failed to save infos");
                return Json(new { Message = ex.Message, ModelState = ModelState });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed.", ModelState = ModelState });
        }
        
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) 
        {
        }
    }
}
