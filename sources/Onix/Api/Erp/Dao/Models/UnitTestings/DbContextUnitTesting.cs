using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Onix.Api.Utils;

namespace Onix.Api.Erp.Dao.Models.UnitTesting
{
    public partial class DbContextUnitTesting : DbContext
    {
        public DbContextUnitTesting(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public virtual DbSet<MUnitTesting> UnitTestingTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
