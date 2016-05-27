using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebAPI.Models;
using Microsoft.Extensions.Logging;
using WebAPI.ViewModels;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommunauteController : Controller
    {
        private INGCookingRepository _ngCookingRepository;
        private Communaute _communaute;
        private ILogger<Communaute> _logger;

        public CommunauteController(INGCookingRepository iNGCookingRep, ILogger<Communaute> logger)
        {
            _ngCookingRepository = iNGCookingRep;
            _communaute = new Communaute();
            _logger = logger; 
        }
        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            Object result = _ngCookingRepository.FindById(1,"Communaute");
            //return Json(_ngCookingRepository.GetAll<Communaute>(_communaute));
            return Json(((Communaute)result));  
        }
       /* public Dictionary<String, Object> parse(byte[] json)
        {
            string jsonStr = Encoding.UTF8.GetString(json);
            return JsonConvert.DeserializeObject<Dictionary<String, Object>>(jsonStr);  
        }*/
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]ICollection<CommunauteViewModel> communautesVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newRecette = Mapper.Map<Recette>(recetteVM); 
                    Response.StatusCode = (int)HttpStatusCode.Created;   
                    _logger.LogInformation("adding successfuly");
                    foreach (var communaute in communautesVM)
                    {
                        //var pic = FileToByteArray(communaute.Picture); 
                        var newCommunaute = new Communaute() {
                            Bio = communaute.Bio,
                            Birth = communaute.Birth,
                            City = communaute.City,
                            Email = communaute.Email,
                            Firstname = communaute.Firstname, 
                            Surname = communaute.Surname,
                            Level = communaute.Level,
                            Password = communaute.Password, 
                            Picture =  _ngCookingRepository.FileToByteArray("ngCooking/"+communaute.Picture)
                        };
                        _ngCookingRepository.Add<Communaute>(newCommunaute); 
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
