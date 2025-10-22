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
    internal class Planconfigurtions : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(50);

            builder.Property(x => x.Description)
                 .HasMaxLength(200);

            builder.Property(x => x.Price)
                 .HasPrecision(10,2);


            builder.ToTable(tb =>
            {

                tb.HasCheckConstraint("Plandurationvheck", "DurationDayes BetWeen 1 and 365"); 

            }); 



        }
    }
}
