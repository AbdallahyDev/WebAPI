using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Communaute : IdentityUser
    {
        //Second validation in addition of  that made by angular 
        [Required(ErrorMessage ="{0} est obligatoire.")]
        [StringLength(20, MinimumLength =3, ErrorMessage ="{0} doit être compris entre {2} et {1} characteres.")] 
        public string Firstname { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")] 
        public string Surname { get; set; }  
        public int Level { get; set; }  
        public Byte[] Picture { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")]
        public string City { get; set; }
        [Required(ErrorMessage = "{0} est obligatoire.")] 
        public int Birth { get; set; }
        [Required]
        [StringLength(2048, MinimumLength = 20, ErrorMessage = "{0} doit être compris entre {2} et {1} characteres.")]
        public string Bio { get; set; }    
    }
}
