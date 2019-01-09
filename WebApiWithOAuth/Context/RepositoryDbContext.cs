using System;
using Microsoft.EntityFrameworkCore;

namespace WebApiWithOAuth.Context
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Produto>()
                //.HasKey(p => p.CodigoBarras);
        }

        //public DbSet<Produto> Produtos { get; set; }
    }
}
