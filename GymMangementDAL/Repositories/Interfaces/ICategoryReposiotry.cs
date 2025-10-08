using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    internal interface ICategoryReposiotry
    {

        IEnumerable<Category> GetAll();



        Category? GetbyId(int id);



        int Add(Category category
            );


        int Update(Category category);



        int Delete(Category category);
    }
}
