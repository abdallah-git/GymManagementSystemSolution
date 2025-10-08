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
    internal class Gymuserconguirtions<T> : IEntityTypeConfiguration<T> where T : Gymuser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {


            builder.Property(x => x.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

            builder.Property(x => x.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(x => x.Phone)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);





            //mosatafa@gmail.com

            builder.ToTable(Tb =>
            {

                Tb.HasCheckConstraint("Gymuservalidemailcheh","Email Like '_%@_%._%' ");
                Tb.HasCheckConstraint("Gymuservalidphonecheh", "Phone Like '01%' and Phone Not Like '%[^0-9]%' ");

            });


            // non clusterd index 
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();


            builder.OwnsOne(x => x.Address, Addressbuilder =>
            {
                Addressbuilder.Property(x => x.Street)
                              .HasColumnName("Street")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);



                Addressbuilder.Property(x => x.City)
                              .HasColumnName("City")
                              .HasColumnType("varchar")
                              .HasMaxLength(30);


                Addressbuilder.Property(x => x.BuildingNumber)
                              .HasColumnName("BuildingNumber"); 
                          
            }); 






        }
    }
}
