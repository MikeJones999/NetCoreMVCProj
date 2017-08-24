using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContext: DbContext
    {
        private IConfigurationRoot _config;

        //IConfig - obtained and injected from startUp.cs
        //DBContextOptions required for OnConfiguring to work - 
        public WorldContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            this._config = config;
        }


        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //ensures the Sql server is being used - providing the connection string (string is in config.json)
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }

    }
}
