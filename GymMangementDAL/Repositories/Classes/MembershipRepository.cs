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
    public class MembershipRepository : GenaricRepository<Membership>, IMembershipRepository
    {
        private readonly GymDbcontext gymDbcontext1; 
        public MembershipRepository(GymDbcontext gymDbcontext ) : base (gymDbcontext)
        {
            gymDbcontext1 = gymDbcontext; 
        }


        public IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null)
        {
            var memberships = gymDbcontext1.Memberships.Include(m => m.Member).Include(m => m.Plan)
                           .Where(filter ?? (_ => true));

            return memberships;
        }


        public Membership? GetFirstOrDefault(Func<Membership, bool>? filter = null)
        {
            var membership = gymDbcontext1.Memberships.Include(m => m.Member).Include(m => m.Plan)
                            .FirstOrDefault(filter ?? (_ => true));
            return membership;
        }


    }
}
