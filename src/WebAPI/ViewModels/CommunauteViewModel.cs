using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class CommunauteViewModel
    {
       
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public String Picture { get; set; }
        public string City { get; set; }
        public int Birth { get; set; } 
        public string Bio { get; set; }
    }
}
