using GymMangementDAL.Data.Contexts;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Classes
{
    internal class CategoryReposiotory : ICategoryReposiotry
    {
        private readonly GymDbcontext dbcontext;
        public CategoryReposiotory(GymDbcontext gym)
        {
            dbcontext = gym; 
        }
        public int Add(Category category)
        {
            dbcontext.Categories.Add(category);
            return dbcontext.SaveChanges(); 
        }

        public int Delete(Category category)
        {
            dbcontext.Categories.Remove(category);
            return dbcontext.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return dbcontext.Categories.ToList(); 
        }

        public Category? GetbyId(int id)
        {
            return dbcontext.Categories.Find(id); 
        }

        public int Update(Category category)
        {
            dbcontext.Categories.Update(category);
            return dbcontext.SaveChanges();
        }
    }
}
