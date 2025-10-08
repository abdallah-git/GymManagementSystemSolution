using GymMangementDAL.Data.Contexts;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Classes
{
    internal class MembesessionReposiotry : IMembersessionReposiotry
    {

        private readonly GymDbcontext dbcontext;
        public MembesessionReposiotry(GymDbcontext gym )
        {
            dbcontext = gym; 
        }
        public int Add(Membersession membersession)
        {
            dbcontext.Membersessions.Add(membersession);
            return dbcontext.SaveChanges();
        }

        public int Delete(Membersession membersession)
        {
            dbcontext.Membersessions.Remove(membersession);
            return dbcontext.SaveChanges();
        }

        public IEnumerable<Membersession> GetAll()
        {
            return dbcontext.Membersessions.ToList(); 
        }

        public Membersession? GetbyId(int id)
        {
            return dbcontext.Membersessions.Find(id); 
        }

        public int Update(Membersession membersession)
        {
            dbcontext.Membersessions.Update(membersession);
            return dbcontext.SaveChanges();
        }
    }
}
