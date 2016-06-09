using System;
using System.Linq;

namespace WebAPI.Models
{
    public interface INGCookingRepository
    {
         string Add<T>(T entity);     
         string Delete<T>(ref T entity);
        string Edit<T>(T entity);
        Object FindById(int id, string tableName);
        Object FindByName(string name, string tableName);
        IQueryable<Object> GetAll<T>(T entity);
        Byte[] FileToByteArray(string fileName);
        IQueryable<Comment> GetCommentsByRecetteId(int id);
        IQueryable<Ingredient> GetIngredientsByRecetteId(int id);
    }
}
