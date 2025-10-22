using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

   
        IGenareicReposiotry<Tentity> GetRepository<Tentity>() where Tentity : BaseEntity, new();

        public ISessionRepository sessionRepository { get; } 

        int Savechanges(); 



    }
}
