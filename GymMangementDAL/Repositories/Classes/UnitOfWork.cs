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
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbcontext _dbcontext;
        

        public UnitOfWork(GymDbcontext dbcontext , ISessionRepository repository , IMembershipRepository membershipRepository , IBookingRepository bookingRepository  )
        {
            _dbcontext = dbcontext;
            sessionRepository = repository;
            MembershipRepository = membershipRepository;
            BookingRepository = bookingRepository;
           
            
        }

        public ISessionRepository sessionRepository { get; }
        public IMembershipRepository MembershipRepository { get; }

        public IBookingRepository BookingRepository { get; }


        public IGenareicReposiotry<Tentity> GetRepository<Tentity>() where Tentity : BaseEntity, new()
        {
            var Entitytype = typeof(Tentity); 

            if (_repositories.ContainsKey(Entitytype)) 
                return (IGenareicReposiotry<Tentity>) _repositories[Entitytype];


            var NewRepo = new GenaricRepository<Tentity>(_dbcontext);
            _repositories[Entitytype] = NewRepo;
            return NewRepo; 


        }

        public int Savechanges()
        {

          return   _dbcontext.SaveChanges(); 
        }
    }
}
