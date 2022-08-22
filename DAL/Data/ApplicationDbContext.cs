using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Metadados;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Text.RegularExpressions;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Nota> Nota { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ToSnakeNames(builder);
        }

        public static void ToSnakeNames(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = ToSnakeCase(entity.GetTableName());
                entity.SetTableName(tableName);

                foreach (var property in entity.GetProperties())
                {
                    var columnName = ToSnakeCase(property.GetColumnName());
                    property.SetColumnName(columnName);
                }

                foreach (var key in entity.GetKeys())
                {
                    var keyName = ToSnakeCase(key.GetName());
                    key.SetName(keyName);
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    var foreignKeyName = ToSnakeCase(key.GetConstraintName());
                    key.SetConstraintName(foreignKeyName);
                }

                foreach (var index in entity.GetIndexes())
                {
                    var indexName = ToSnakeCase(index.GetName());
                    index.SetName(indexName);
                }
            }
        }

        private static string ToSnakeCase(string name)
        {
            return string.IsNullOrWhiteSpace(name)
                ? name
                : Regex.Replace(
                    name,
                    @"([a-z0-9])([A-Z])",
                    "$1_$2",
                    RegexOptions.Compiled,
                    TimeSpan.FromSeconds(0.2)).ToLower();
        }

    }
}
