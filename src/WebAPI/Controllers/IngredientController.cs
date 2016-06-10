using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using WebAPI.Models;
using Microsoft.Extensions.Logging;
using WebAPI.ViewModels;
using System.Net;
using AutoMapper;
using WebAPI.Models.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860 
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class IngredientController : Controller
    {
        private ILogger<Ingredient> _logger;
        private INGCookingRepository _ngCookingRepository;
        private Ingredient _ingredient; 
        public IngredientController(INGCookingRepository iNGCookingRep, ILogger<Ingredient> logger)
        {
            _ngCookingRepository = iNGCookingRep;
            _ingredient = new Ingredient(); 
            _logger = logger;
        }
        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_ngCookingRepository.GetAll<Ingredient>(_ingredient)); 
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]ICollection<IngredientViewModel> ingredientsVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.Created; 
                    _logger.LogInformation("adding successfuly"); 
                    foreach (var ingredient in ingredientsVM)
                    {
                        var newIngredient = Mapper.Map<Ingredient>(ingredient);
                        newIngredient.Picture = _ngCookingRepository.FileToByteArray("ngCooking/img/ingredients/" + ingredient.Picture);    
                        _ngCookingRepository.Add<Ingredient>(newIngredient);       
                    }
                    return Json("It is succesfully added"); 
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
