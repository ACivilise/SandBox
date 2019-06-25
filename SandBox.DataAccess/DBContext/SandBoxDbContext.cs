using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SandBox.DataAccess.Entities.Geographie;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.DataAccess.DBContext
{
    public class SandBoxDbContext : IdentityDbContext, ISandBoxDbContext
    {

        // Utilisé par l’implémentation de IDesignTimeDbContextFactory
        public SandBoxDbContext(DbContextOptions<SandBoxDbContext> options)
            : base(options)
        {

        }

        #region DbSets
        public DbSet<CodePostal> CodePostaux { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Pays> Pays { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Ville> Villes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
