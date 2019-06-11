using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web90TvCore2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsers, ApplicationRoles, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Advertise> Advertises { get; set; }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<PollOption> PollOptions { get; set; }

    }
}
