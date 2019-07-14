using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using Onix.Api.Utils;

namespace Onix.Api.Erp.Dao.Models
{
    public partial class OnixDbContext : DbContext
    {
        private LibSetting setting = null;

        public OnixDbContext()
        {
            setting = LibSetting.GetInstance();
        }

        public OnixDbContext(DbContextOptions<DbContext> options) : base(options)
        { 
            //In memory mode, no OnConfigurring() will be call    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(setting.ConnectionString);
                optionsBuilder.UseLoggerFactory(setting.LogFactory); 

                base.OnConfiguring(optionsBuilder);               
            }
        }

        public virtual DbSet<MMasterRef> MasterRef { get; set; }
        public virtual DbSet<MEmployee> Employee { get; set; }
    }
}
