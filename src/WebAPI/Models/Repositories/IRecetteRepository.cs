using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public interface IRecetteRepository
    {
         Recette getRecetteById(); 
         long AddRecette(Recette recette);
    }
}
