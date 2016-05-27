using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class RecetteRepository : IRecetteRepository
    {
        private DBContext _contx;

        public RecetteRepository(DBContext dbContext)
        {
            _contx = dbContext;  
        } 
        public long AddRecette(Recette recette)
        {
            _contx.Recettes.Add(recette);   
            _contx.SaveChanges(); 
            return recette.Id;  
        } 

        public Recette getRecetteById()
        {
            throw new NotImplementedException();
        }
    }
}
