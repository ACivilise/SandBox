using Microsoft.EntityFrameworkCore;
using SandBox.DataAccess.Entities.Geographie;
using SandBox.DataAccess.Entities.Weather;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.DataAccess.DBContext
{
    public interface ISandBoxDbContext
    {
        DbSet<ZipCode> ZipCodes { get; }
        DbSet<Departement> Departements { get; }
        DbSet<Country> Countries { get; }
        DbSet<Region> Regions { get; }
        DbSet<City> Cities { get; }
        DbSet<Temperatures> Temperatures { get; }
    }
}
