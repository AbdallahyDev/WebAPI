using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using System.Net;
using WebAPI.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private ILogger<Comment> _logger;
        private INGCookingRepository _ngCookingRepository;
        private Comment _comment;

        public CommentController(INGCookingRepository iNGCookingRep, ILogger<Comment> logger)
        {
            _ngCookingRepository = iNGCookingRep;  
            _comment = new Comment();
            _logger = logger;
        }
        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            
            return Json(_ngCookingRepository.GetAll<Comment>(_comment));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            //return Json(_ngCookingRepository.FindById(int.Parse(id),"Comment"));
            
            try
            {
                if (ModelState.IsValid)
                {
                    //var newRecette = Mapper.Map<Recette>(recetteVM); 
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    var res = _ngCookingRepository.GetCommentsByRecetteId(int.Parse(id));  
                    _logger.LogInformation($"adding successfuly {res.Count()}");      
                    return Json(res);  
                    //return Json("it is succesfully added"); 
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

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]CommentViewModel commentVM) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    Comment newComment = new Comment() { CommentBody = commentVM.Comments, Mark = commentVM.Mark, Title = commentVM.Title,
                        UserId = commentVM.UserId,
                        Recette = (Recette)_ngCookingRepository.FindById(commentVM.RecetteId,"Recette") 
                    };
                    _ngCookingRepository.Add<Comment>(newComment);  
                    return Json("comment successfully added");  
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
