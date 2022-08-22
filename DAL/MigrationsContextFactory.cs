using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class MigrationsContextFactory : IDesignTimeDbContextFactory<MigrationsDbContext>
    {
        public MigrationsContextFactory() { }

        public MigrationsDbContext CreateDbContext(string[] args)
        {
            string connectionString;

            if (args.Length > 0)
            {
                connectionString = args[0];
            }
            else
            {
                var configBuilder = new ConfigurationBuilder();
                var config = configBuilder.Build();

                connectionString = "Host=localhost;Username=postgres;Password=master;port=5432;Database=GestaoPatrimonio";
            }

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.EnableSensitiveDataLogging(true);

            builder.UseNpgsql(connectionString, x =>
            {
                x.MigrationsHistoryTable("migration_history", "public");
            });

            var context = new MigrationsDbContext(builder.Options);

            return context;
        }
    }
}
