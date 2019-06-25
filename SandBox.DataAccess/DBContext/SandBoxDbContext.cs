using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SandBox.DataAccess.Entities.Geographie;
using SandBox.DataAccess.Entities.Weather;
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
        public DbSet<ZipCode> ZipCodes { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Temperatures> Temperatures { get; set; }

        
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
