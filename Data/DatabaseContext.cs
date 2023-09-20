using System;
using Microsoft.EntityFrameworkCore;
using BrasilApiApp.Models;

namespace BrasilApiApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        // Adicione outros DbSets conforme necessário

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de modelo vão aqui
        }
    }
}
