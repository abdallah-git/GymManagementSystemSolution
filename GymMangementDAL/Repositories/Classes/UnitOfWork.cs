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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbcontext _dbcontext;
        

        public UnitOfWork(GymDbcontext dbcontext , ISessionRepository repository  )
        {
            _dbcontext = dbcontext;
            sessionRepository = repository; 
            
        }

        public ISessionRepository sessionRepository { get; }






        private readonly Dictionary<Type, object> _repository = new () ;


        public IGenareicReposiotry<Tentity> GetRepository<Tentity>() where Tentity : BaseEntity, new()
        {
            var Entitytype = typeof(Tentity); 

            if (_repository.ContainsKey(Entitytype)) 
                return (IGenareicReposiotry<Tentity>) _repository[Entitytype];


            var NewRepo = new GenaricRepository<Tentity>(_dbcontext);
            _repository[Entitytype] = NewRepo;
            return NewRepo; 


        }

        public int Savechanges()
        {

          return   _dbcontext.SaveChanges(); 
        }
    }
}
