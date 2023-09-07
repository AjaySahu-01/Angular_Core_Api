using Angular_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Angular_Ecommerce.Context
{
    public class UserDbContext: DbContext
        {
            public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {


        }
        public DbSet<User> users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        
    }
}
 
