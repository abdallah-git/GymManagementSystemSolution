using GymMangementDAL.Data.Contexts;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Classes
{
    public class GenaricRepository <Tentity> : IGenareicReposiotry<Tentity> where Tentity : BaseEntity, new()

    {
        private readonly GymDbcontext dbcontext1; 
        public GenaricRepository(GymDbcontext dbcontext )
        {
            dbcontext = dbcontext1; 
        }
        public void Add(Tentity entity)
        {
            dbcontext1.Set<Tentity>().Add(entity);
            
        }

        public void Delete(Tentity entity)
        {
            dbcontext1.Set<Tentity>().Remove(entity); 
         
        }

        public IEnumerable<Tentity> GetAll(Func<Tentity, bool>? condition = null)
        {
            if (condition is null)
                return dbcontext1.Set<Tentity>().AsNoTracking().ToList();
            else
                return dbcontext1.Set<Tentity>().AsNoTracking().Where(condition).ToList(); 
        }

        public Tentity GetById(int id)
        {
            return dbcontext1.Set<Tentity>().Find(id); 
        }

        public void Update(Tentity entity)
        {
            dbcontext1.Set<Tentity>().Update(entity);
            
        }
    }
}
