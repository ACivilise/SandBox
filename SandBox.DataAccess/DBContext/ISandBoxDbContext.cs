using Microsoft.EntityFrameworkCore;
using SandBox.DataAccess.Entities.Geographie;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.DataAccess.DBContext
{
    public interface ISandBoxDbContext
    {
        DbSet<CodePostal> CodePostaux { get; }
        DbSet<Departement> Departements { get; }
        DbSet<Pays> Pays { get; }
        DbSet<Region> Regions { get; }
        DbSet<Ville> Villes { get;  }
    }
}
