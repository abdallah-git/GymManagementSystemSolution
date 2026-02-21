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
    public class BookingRepository : GenaricRepository<Membersession>, IBookingRepository
    {

        private readonly GymDbcontext _context;

        public BookingRepository(GymDbcontext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Membersession> GetSessionById(int sessionId)
        {
            return _context.Membersessions.Where(ms => ms.SessionId == sessionId)
                                          .Include(ms => ms.Member)
                                          .ToList();
        }




    }
}
