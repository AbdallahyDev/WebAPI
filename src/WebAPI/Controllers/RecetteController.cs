using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net;
using WebAPI.ViewModels;
using WebAPI.Models;
using AutoMapper;
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
            //return Json(_ngCookingRepository.FindById(1, "Recette"));
            return Json(_ngCookingRepository.GetAll<Recette>(_recette));  
        }

        // GET api/values/5
        [HttpGet("{id}")] 
        public string Get(int id)
        {
            return "value"; 
        }
        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]ICollection <RecetteViewModel> recettesVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newRecette = Mapper.Map<Recette>(recetteVM); 
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    _logger.LogInformation("adding successfuly");
                    foreach(var recette in recettesVM)
                    {
                        var newRecette = new Recette() {Calories=recette.Calories, CreatorId=recette.CreatorId, IsAvailable = recette.IsAvailable, Name=recette.Name, Preparation=recette.Preparation,
                            CategoryName = ""
                        };
                        foreach (var ing in recette.Ingredients) { 
                            
                        }
                    }
                    //return Json(newRecette); 
                    //return Json(CreatedAtRoute("GetTodo", new { controller = "RecetteCnt" }, recetteVM));

                     return Json("it works");     
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
