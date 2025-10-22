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
    public class SessionReposiotry : GenaricRepository<Session>, ISessionRepository
    {

        private readonly GymDbcontext gymDbcontext;  
        public SessionReposiotry(GymDbcontext dbcontext ) : base (dbcontext) 
        {
            gymDbcontext = dbcontext; 
        }
        public IEnumerable<Session> GetAllsesionswithtrainersandcategories()
        {
            return gymDbcontext.Sessions.Include(x => x.SessionTrainer)
                                        .Include(x => x.sessioncategory)
                                        .ToList(); 
        }

        public int GetCountofbookslots(int sessionid)
        {


            return gymDbcontext.Membersessions.Count(x => x.SessionId == sessionid); 




        }

        public Session? GetSessionByIDwithtrainersandcategories(int id)
        {
            return gymDbcontext.Sessions.Include(x => x.SessionTrainer)
                                        .Include(x => x.sessioncategory)
                                        .FirstOrDefault(x => x.Id == id); 
                                       

        }
    }
}
