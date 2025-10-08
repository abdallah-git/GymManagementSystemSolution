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
    internal class SessionReposiotry : ISessionReposiotry
    {

        private readonly GymDbcontext dbcontext;

        public SessionReposiotry(GymDbcontext gym )
        {
            dbcontext = gym; 
            
        }
        public int Add(Session session)
        {
            dbcontext.Sessions.Add(session);
            return dbcontext.SaveChanges(); 
        }

        public int Delete(Session session)
        {
            dbcontext.Sessions.Remove(session);
           return  dbcontext.SaveChanges();
        }

        public IEnumerable<Session> GetAll()
        {
          return  dbcontext.Sessions.ToList(); 
        }

        public Session? GetbyId(int id)
        {
            return dbcontext.Sessions.Find(id); 
        }

        public int Update(Session session)
        {
            dbcontext.Sessions.Update(session);
            return dbcontext.SaveChanges(); 
        }
    }
}
