using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using WebAPI.Models;
using Microsoft.Extensions.Logging;
using WebAPI.ViewModels;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
//Microsoft.AspNet.Server.Kestrel

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ILogger<Category> _logger;
        private INGCookingRepository _ngCookingRepository; 
        private Category _category;

        public CategoryController(INGCookingRepository iNGCookingRep, ILogger<Category> logger)
        {
            _ngCookingRepository = iNGCookingRep;  
            _category = new Category();
            _logger = logger;
        }
        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_ngCookingRepository.GetAll<Category>(_category)); 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]ICollection<CategoryViewModel> categoriesVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newRecette = Mapper.Map<Recette>(recetteVM); 
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    _logger.LogInformation("adding successfuly");
                    foreach (var category in categoriesVM)
                    {
                        var newCategory = new Category(){Name = category.NameToDisplay}; 
                        _ngCookingRepository.Add<Category>(newCategory);   
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
