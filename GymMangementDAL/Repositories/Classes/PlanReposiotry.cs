using GymMangementDAL.Data.Contexts;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymMangementDAL.Repositories.Classes
{
    internal class PlanReposiotry : IPlanReopositery
    {
        private readonly GymDbcontext dbcontext;
        public PlanReposiotry(GymDbcontext gym)
        {
            dbcontext = gym; 
        }
        public int Add(Plan plan)
        {
            dbcontext.Plans.Add(plan);
            return dbcontext.SaveChanges();

        }

        public int Delete(Plan plan)
        {
            dbcontext.Plans.Remove(plan);
            return dbcontext.SaveChanges();
        }

        public IEnumerable<Plan> GetAll()
        {
            return dbcontext.Plans.ToList(); 
        }

        public Plan? GetbyId(int id)
        {
            return dbcontext.Plans.Find(id); 
            
        }

        public int Update(Plan plan)
        {
            dbcontext.Plans.Update(plan);
            return dbcontext.SaveChanges();

        }
    }
}

