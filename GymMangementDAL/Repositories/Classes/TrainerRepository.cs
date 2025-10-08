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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbcontext dbcontext ;
        public TrainerRepository(GymDbcontext gymDbcontext)
        {
            dbcontext = gymDbcontext; 
        }
        public int Add(Trainer trainer)
        {
            dbcontext.Add(trainer);
            return dbcontext.SaveChanges(); 
        }

        public int Delete(Trainer trainer)
        {
            dbcontext.Trainers.Remove(trainer);
            return dbcontext.SaveChanges(); 
        }

        public IEnumerable<Trainer> GetAll()
        {
            return dbcontext.Trainers.ToList(); 
        }

        public Trainer GetTrainerbyId(int id)
        {
            return dbcontext.Trainers.Find(id); 
        }

        public int Update(Trainer trainer)
        {
            dbcontext.Trainers.Update(trainer);
            return dbcontext.SaveChanges(); 
        }
    }
}
