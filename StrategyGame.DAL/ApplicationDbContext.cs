using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.DAL
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public override DbSet<User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Create Db Scheme

            //Seed
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();

            var user1 = new User
            {
                UserName = "Rajmundo1"
            };
            user1.PasswordHash = passwordHasher.HashPassword(user1, "Password1");

            builder.Entity<User>().HasData(user1);
        }
    }
}
