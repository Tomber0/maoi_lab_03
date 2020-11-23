using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace maoi_lab_03
{
    class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }


        /*            public DbSet<StandartModel> Standart { get; set; }
                    public DbSet<ImageLetterModel> Image { get; set; }
        */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-C97C4J73\\SQLEXPRESS;Database=Lab3;Trusted_Connection=True;");//LAPTOP-C97C4J73\\SQLEXPRESS
        }
    }
}
