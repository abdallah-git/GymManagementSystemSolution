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
    internal class MembershipReposiotry : IMembershipReposiotry
    {
        private readonly GymDbcontext dbcontext;
        public MembershipReposiotry(GymDbcontext gym )
        {
            dbcontext = gym; 
        }
        public int Add(Membership membership)
        {
            dbcontext.Memberships.Add(membership);
            return dbcontext.SaveChanges();
        }

        public int Delete(Membership membership)
        {
            dbcontext.Memberships.Remove(membership);
            return dbcontext.SaveChanges();
        }

        public IEnumerable<Membership> GetAll()
        {
            return dbcontext.Memberships.ToList(); 
        }

        public Membership? GetbyId(int id)
        {
            return dbcontext.Memberships.Find(id); 
        }

        public int Update(Membership membership)
        {
            dbcontext.Memberships.Update(membership);
            return dbcontext.SaveChanges();

        }
    }
}
