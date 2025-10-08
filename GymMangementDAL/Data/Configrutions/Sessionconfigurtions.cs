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
    internal class Sessionconfigurtions : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {

            builder.ToTable(tb =>
            {

                tb.HasCheckConstraint("sessioncapictyvalid ", "Capacity BetWeen 1 and 25 ");
                tb.HasCheckConstraint("sessiondatecheck ",   "EndDate > StartDate ");

            });



            builder.HasOne(x => x.sessioncategory)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.CategoryId);



            builder.HasOne(x => x.SessionTrainer)
               .WithMany(x => x.TrainerSessions)
               .HasForeignKey(x => x.TrainerId);






        }
    }
}
