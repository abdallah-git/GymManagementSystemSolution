using GymMangementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Data.Configrutions
{
    internal class Membersessionconfigurtion : IEntityTypeConfiguration<Membersession>
    {
        public void Configure(EntityTypeBuilder<Membersession> builder)
        {



            builder.Property(x => x.CreatedAt)
               .HasColumnName("BookingDate")
               .HasDefaultValueSql("GETDATE()");



            builder.HasKey(x => new { x.MemberId, x.SessionId }); 
            builder.Ignore(x => x.Id);

            

        }
    }
}
