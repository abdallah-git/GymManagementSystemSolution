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
    internal class Healthrecordconfigurtions : IEntityTypeConfiguration<Healthrecord>
    {
        public void Configure(EntityTypeBuilder<Healthrecord> builder)
        {

            builder.ToTable("Members")
                .HasKey(x => x.Id);


            builder.HasOne<Member>()
                 .WithOne(x => x.Healthrecord)
                 .HasForeignKey<Healthrecord>(x => x.Id);

            builder.Ignore(x => x.CreatedAt);
            builder.Ignore(x => x.UpdatedAt); 




        }
    }
}
