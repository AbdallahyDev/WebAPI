using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using System.Net;
using WebAPI.ViewModels;
using WebAPI.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Data.Entity;
using WebAPI.Models.Repositories;

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
            List<Object> recettes = _ngCookingRepository.GetAll<Recette>(_recette).ToList();
            List<RecetteViewModel> recettesVM = new List<RecetteViewModel>();  
            foreach (Recette recette in recettes)
            {
                List<Ingredient> recetteIngredients = _ngCookingRepository.GetIngredientsByRecetteId(recette.Id).ToList();
                List<Comment> recetteComments = _ngCookingRepository.GetCommentsByRecetteId(recette.Id).ToList();
                var recetteVM = Mapper.Map<RecetteViewModel>(recette);  
                _logger.LogInformation("Le mappage est fait");
                recetteVM.Ingredients = new List<Ingredient>();
                recetteVM.Comments = new List<Comment>(); 
                //formattage de ingredients
                foreach (Ingredient ingredient in recetteIngredients)
                {
                    recetteVM.Ingredients.Add((Ingredient)_ngCookingRepository.FindById(ingredient.Id, "Ingredient"));  
                }
                //formattage de comments
                foreach (Comment comment in recetteComments) 
                {
                    recetteVM.Comments.Add(comment); 
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
               // var pic = this.HttpContext.Request.Form.Count();//.Files["picture"].OpenReadStream();//l'image à charger  
                                                                // HttpRequestMessage request = HttpContext.Request;
                //if (!Request.Content.IsMimeMultipartContent())
                //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

                //var provider = new MultipartMemoryStreamProvider();
                //await Request.Form..Content.ReadAsMultipartAsync(provider);
                //foreach (var file in provider.Contents)
                //{
                //    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                //    var buffer = await file.ReadAsByteArrayAsync();
                //    //Do whatever you want with filename and its binaray data.
                //}

                //var request = this.HttpContext.Request;
                //var filePath = "C:\\temp\\" + request.Headers["picture"]; 
                //using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))   
                //{
                //   // request..InputStream.CopyTo(fs);
                //}


                if (ModelState.IsValid) 
                {
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    _logger.LogInformation($"adding successfuly:{recetteVM.Name}");
                    var newRecette = Mapper.Map<Recette>(recetteVM);
                    _ngCookingRepository.Add<Recette>(newRecette);
                    RecetteIngredient newRecetteIngredient = null;
                    foreach (var ing in recetteVM.Ingredients)
                    {
                        object ingredient = _ngCookingRepository.FindByName(ing.Name, "Ingredient");
                        newRecetteIngredient = new RecetteIngredient() { IngredientId = ((Ingredient)ingredient).Id, RecetteId = ((Recette)_ngCookingRepository.FindByName(newRecette.Name, "Recette")).Id };
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
