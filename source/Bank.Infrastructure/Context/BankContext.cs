using Bank.Domain.Entities;
using Bank.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Context
{
    public class BankContext : DbContext
    {
        public BankContext(){}

        public BankContext(DbContextOptions<BankContext> options) : base(options){}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(@"Data Source=DESKTOP-I3IRLUG;Initial Catalog=BANK;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer();

    }
}