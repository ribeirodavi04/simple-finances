using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure.Context
{
    public class SimpleFinancesDbContext : DbContext
    {

        public SimpleFinancesDbContext(DbContextOptions<SimpleFinancesDbContext> options) : base(options)
        {

        }

        public DbSet<Cartao> Cartoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleFinancesDbContext).Assembly);

        }
    }
}
