using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAPI.Models
{
    public class Communaute : IdentityUser
    {
        //The data in addition for an user
        //Validation is not verify her because we use angular validation
        public string Firstname { get; set; }    
        public string Surname { get; set; }  
        public int Level { get; set; }  
        public Byte[] Picture { get; set; }
        public string City { get; set; }  
        public int Birth { get; set; }  
        public string Bio { get; set; }   
    }
}
