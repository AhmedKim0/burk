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

        public DbSet<Client> Clients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TempUser> TempUsers { get; set; }
        public DbSet<WaitingList> WaitingLists { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Question> Questions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
		{
            builder.Entity<Question>().HasNoKey();
			base.OnModelCreating(builder);
		}


	}

    
 }
