using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Data.Entity;

namespace WebAPI.Models.Repositories
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
            if (type.Equals(typeof(Recette)))
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
        
        public string Delete<T>(T entity)
        {
            Object t = entity; 
            Type type = entity.GetType();
            if (type.Equals(typeof(Communaute)))
            {
                _cntx.Users.Remove((Communaute)t); 
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

        //method for geting a byte array from an image url
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
                    res= _cntx.Users.Single(x =>x.Id == id.ToString());     
                    break; 
                case "Comment":
                    res = _cntx.Comments.Single(x => x.Id == id); 
                    break;
                case "Recette": 
                    res = _cntx.Recettes.Single(x => x.Id == id); 
                    break;
                case "Ingredient":
                    res = _cntx.Ingredients.Single(x => x.Id == id);
                    break;
                case "Category":
                    res = _cntx.Categories.Single(x => x.Id == id);     
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
                    res = _cntx.Users.Single(x => x.Firstname == name); 
                    break;
                case "Comment":
                    res = _cntx.Comments.Single(x => x.Title == name);  
                    break;
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

        public IQueryable<Comment> GetCommentsByRecetteId(int id)
        {
            IQueryable<Comment> comments = _cntx.Comments.FromSql("SELECT * FROM dbo.Comment where RecetteId="+id); 
            return comments;    
        }
        public IQueryable<Ingredient> GetIngredientsByRecetteId(int id)
        {
            var res = _cntx.RecetteIngredient
                .Where(x => x.RecetteId == id)   
                .Select(x => new Ingredient
                {  Id=x.IngredientId
                });
            return res;
        }
        
        public IQueryable<Object> GetAll<T>(T entity) 
        {
            Type type = entity.GetType(); 
            IQueryable<Object> results = null;     
            if (type.Equals(typeof(Communaute)))
            {
                results = _cntx.Users.OrderBy(c => c.Firstname);    
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
 
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText); 
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray()); 
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream()) 
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
