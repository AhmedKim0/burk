using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Context
{
    public class BurkDbContext : IdentityDbContext
    {
        public BurkDbContext(DbContextOptions<BurkDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Registrations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AvaliableTable> AvaliableTabels { get; set; }
        public DbSet<WaitingList> WaitingLists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvaliableTable>().HasNoKey(); // Indicates no primary key. }
        }
    }
}
