using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class NGCookingRepository : INGCookingRepository
    {
        private DBContext _cntx;
        public NGCookingRepository(DBContext cntx)
        {
            _cntx = cntx;  
        }
        public NGCookingRepository()
        {

        }


        public string Add<T>(T entity)
        { 
            Object t = entity;  
            Type type = entity.GetType();
            if (type.Equals(typeof(Communaute)))
            {
                _cntx.Communautes.Add((Communaute)t); 
            }
            else if (type.Equals(typeof(Recette)))
            {
                _cntx.Recettes.Add((Recette)t);
            }
            else if (type.Equals(typeof(Comment)))
            {
                _cntx.Comments.Add((Comment)t);
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                _cntx.Ingredients.Add((Ingredient)t);
            }
            else if (type.Equals(typeof(RecetteIngredient)))
            {
                _cntx.RecetteIngredient.Add((RecetteIngredient)t);
            }
            else if (type.Equals(typeof(Category)))
            {
                _cntx.Categories.Add((Category)t);
            }

            var messRetour = (_cntx.SaveChanges() > 0) ? "entity added" : "adding entity failed";
            return messRetour;
        }
        //method for deleting an entity       
        public string Delete<T>(ref T entity)
        {
            Object t = entity;
            Type type = entity.GetType();
            if (type.Equals(typeof(Communaute)))
            {
                _cntx.Communautes.Remove((Communaute)t);
            }
            else if (type.Equals(typeof(Recette)))
            {
                _cntx.Recettes.Remove((Recette)t);
            }
            else if (type.Equals(typeof(Comment)))
            {
                _cntx.Comments.Remove((Comment)t);
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                _cntx.Ingredients.Remove((Ingredient)t);
            }
            else if (type.Equals(typeof(RecetteIngredient)))
            {
                _cntx.RecetteIngredient.Remove((RecetteIngredient)t);
            }
            else if (type.Equals(typeof(Category)))
            {
                _cntx.Categories.Remove((Category)t);
            }
            _cntx.SaveChanges(); 
            var messRetour = (_cntx.SaveChanges() > 0) ? "entity added" : "adding entity failed";
            return messRetour; 
        }

        public Byte[] FileToByteArray(string fileName)
        {
            byte[] fileContent = null;

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);

            long byteLength = new System.IO.FileInfo(fileName).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent; 
        } 

        public string Edit<T>(T entity)
        {
            throw new NotImplementedException();
        }



        public Object FindById(int id, string tableName)
        {
            object res = null; 
            switch (tableName)
            {
                case "Communaute":
                    res= _cntx.Communautes.Single(x => x.Id == id);
                    break;
                case "Comment":
                    res = _cntx.Comments.Single(x => x.Id == id);
                    break;
                //case "RecetteIngredient":
                //    res = _cntx.RecetteIngredient.Single(x => x.Id == id);
                //    break;
                case "Recette": 
                    res = _cntx.Recettes.Single(x => x.Id == id);
                    break;
                case "Ingredient":
                    res = _cntx.Ingredients.Single(x => x.Id == id);
                    break;
                case "Category":
                    res = _cntx.Categories.Single(x => x.Id == id.ToString());  
                    break;
                default:
                    break; 
            }
            return res;   
        }



        public Object FindByName(string name, string tableName)
        {
            object res = null;
            switch (tableName)
            {
                case "Communaute":
                    res = _cntx.Communautes.Single(x => x.Firstname == name); 
                    break;
                case "Comment":
                    res = _cntx.Comments.Single(x => x.Title == name);  
                    break;
                //case "RecetteIngredient":
                //    res = _cntx.RecetteIngredient.Single(x => x.Id == id);
                //    break;
                case "Recette":
                    res = _cntx.Recettes.Single(x => x.Name == name);
                    break;
                case "Ingredient":
                    res = _cntx.Ingredients.Single(x => x.Name == name);
                    break;
                case "Category":
                    res = _cntx.Categories.Single(x => x.Name == name); 
                    break;
                default:
                    break;
            }
            return res;
        }


        public IQueryable<Object> GetAll<T>(T entity)
        {
            Type type = entity.GetType(); 
            IQueryable<Object> results = null;     
            if (type.Equals(typeof(Communaute)))
            {
                results = _cntx.Communautes.OrderBy(c => c.Id);
            }
            else if (type.Equals(typeof(Recette)))
            {
                results = _cntx.Recettes.OrderBy(c => c.Id);
            }
            else if (type.Equals(typeof(Comment)))
            {
                results = _cntx.Comments.OrderBy(c => c.Id);
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                results = _cntx.Ingredients.OrderBy(c => c.Id);
            }
            else if (type.Equals(typeof(RecetteIngredient)))
            {
                results = _cntx.RecetteIngredient.OrderBy(c => c.RecetteId); 
            }
            else if (type.Equals(typeof(Category)))
            {
                results = _cntx.Categories.OrderBy(c => c.Id);  
            }
            return results;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
