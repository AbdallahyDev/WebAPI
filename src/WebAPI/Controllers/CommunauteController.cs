using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebAPI.Models;
using Microsoft.Extensions.Logging;
using WebAPI.ViewModels;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;
using AutoMapper;
using WebAPI.Models.Repositories;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommunauteController : Controller
    {
        private INGCookingRepository _ngCookingRepository;
        private Communaute _communaute;
        private ILogger<Communaute> _logger;
        private UserManager<Communaute> _communauteManeger;
        private SignInManager<Communaute> _signInManager;

        public CommunauteController(INGCookingRepository iNGCookingRep, ILogger<Communaute> logger, UserManager<Communaute> communauteManeger, SignInManager<Communaute> signInManager)
        {
            _ngCookingRepository = iNGCookingRep;
            _communaute = new Communaute();
            _logger = logger;
            _communauteManeger = communauteManeger;
            _signInManager = signInManager; 
        } 
        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            var communautes = _ngCookingRepository.GetAll<Communaute>(_communaute);
            ICollection<CommunauteViewModel> communautesVM = new List<CommunauteViewModel>(); 
            foreach (Communaute communaute in communautes)
            {
                var communauteVM = Mapper.Map<CommunauteViewModel>(communaute); 
                communautesVM.Add(communauteVM); 
            }
            return Json(communautesVM);    
        }
        
        
        // POST api/values
        
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous] 
        public async Task<JsonResult> Register([FromBody]CommunauteFromViewModel communauteVM)
        {
            try
            { 
                if (ModelState.IsValid)
                {                 
                    Response.StatusCode = (int)HttpStatusCode.Created;     
                    _logger.LogInformation("adding successfuly");
                    var newCommunaute = Mapper.Map<Communaute>(communauteVM); 
                    var result = await _communauteManeger.CreateAsync(newCommunaute, communauteVM.Password);  
                    if (result.Succeeded) 
                    {
                        await _signInManager.SignInAsync(newCommunaute, true);    
                        return Json("successfuly added");  
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);       
                        }
                        return Json(result.Errors.ToList()); 
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError($"failed to save infos:{ex.Message}, stack:{ex.InnerException}"); 
                return Json(new { Message = ex.Message, ModelState = ModelState }); 
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed.", ModelState = ModelState });
        }

        
        [Route("Logout")]
        [HttpPost]
        public async Task<JsonResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                Response.StatusCode = (int)HttpStatusCode.Created;
                _logger.LogInformation("logout successfuly"); 
                return Json("logout successfuly"); 
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError($"failed to save infos:{ex.Message}, stack:{ex.InnerException}");
                return Json(new { Message = ex.Message, ModelState = ModelState });
            }
        }

         
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost] 
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginVM)    
        {
           
            if (ModelState.IsValid) 
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, true, lockoutOnFailure: false);   
                if (result.Succeeded) 
                {
                    _logger.LogInformation(1, "User logged in."); 
                    var currentUserId = User.GetUserId(); 
                    return Json("User logged in succesfully"); 
                }
                if (result.RequiresTwoFactor)
                {
                    return Json("RequiresTwoFactor");
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return Json("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Json("Invalid login attempt.");
                }
            }
            // If we got this far, something failed, redisplay form
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
