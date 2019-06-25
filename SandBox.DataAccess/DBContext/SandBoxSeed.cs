using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SandBox.DataAccess.DBContext.SeedObjects;
using SandBox.DataAccess.Entities.Geographie;
using SandBox.DataAccess.Entities.Weather;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SandBox.DataAccess.DBContext
{
    class SandBoxSeed
    {
        private readonly IServiceProvider _services;
        public SandBoxDbContext Context { get; private set; }
        private readonly ILogger<SandBoxSeed> _logger;

        public SandBoxSeed(SandBoxDbContext context, IServiceProvider services)
        {
            Context = context;
            _services = services;
            _logger = services.GetService<ILogger<SandBoxSeed>>();
        }

        public virtual async Task Run()
        {
            _logger.LogInformation("Début du Seed");

            _logger.LogInformation("Début du seed Countries");
            await InitCountries();
            _logger.LogInformation("Début du seed Region");
            await InitRegion();
            _logger.LogInformation("Début du seed Departments");
            await InitDepartments();
            //Trop long
            //_logger.LogInformation("Début du seed CitiesAndZipCodes");
            // await InitCitiesAndZipCodes();

            await SeedData();
            _logger.LogInformation("Fin du Seed");
        }

        #region initGeo
        private async Task InitCountries()
        {
            if ((await Context.Countries.AnyAsync()) == false)
            {
                Assembly assembly = typeof(SandBoxDbContext).GetTypeInfo().Assembly;
#if DEBUG
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains("countries.json"));
#else
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains("countries_All.json"));
#endif
                _logger.LogInformation($"utilisation du fichier {resourceName}");
                var jsonContent = "";
                var countries = new List<JsonCountry>();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        jsonContent = reader.ReadToEnd();
                        countries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonCountry>>(jsonContent);
                    }
                }
                Context.Countries.AddRange(countries.Select(c => new Country { Code = c.code, ISO2 = c.alpha2, ISO3 = c.alpha3, Libelle = c.nom_fr_fr, LibelleEN = c.nom_en_gb }));
                await Context.SaveChangesAsync();
            }
        }

        private async Task InitRegion()
        {
            if ((await Context.Regions.AnyAsync()) == false)
            {
                Assembly assembly = typeof(SandBoxDbContext).GetTypeInfo().Assembly;
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains("departments.json"));
                _logger.LogInformation($"utilisation du fichier {resourceName}");
                var jsonContent = "";
                var jsonRegions = new List<JsonRegion>();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        jsonContent = reader.ReadToEnd();
                        jsonRegions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonDepartment>>(jsonContent)
                            .Select(jd => new JsonRegion { Code = jd.Region.Code, Title = jd.Region.Title })
                            .GroupBy(jr => jr.Code)
                            .Select(g => g.FirstOrDefault()).ToList();
                    }
                }
                var france = await Context.Countries.Where(c => c.Libelle == "France").FirstOrDefaultAsync();
                var regions = new List<Region>();

                foreach (var jsonRegion in jsonRegions)
                {
                    var region = new Region
                    {
                        Code = jsonRegion.Code,
                        Pays = france,
                        Libelle = jsonRegion.Title,
                    };
                    france.Regions.Add(region);

                    var dep = region.Code;
                    if (region.Code.StartsWith("0"))
                        dep = region.Code.Substring(1);

                    regions.Add(region);
                }

                await Context.Regions.AddRangeAsync(regions);

                await Context.SaveChangesAsync();

            }
        }

        private async Task InitDepartments()
        {
            if ((await Context.Departements.AnyAsync()) == false)
            {
                Assembly assembly = typeof(SandBoxDbContext).GetTypeInfo().Assembly;
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains("departments.json"));
                _logger.LogInformation($"utilisation du fichier {resourceName}");
                var jsonContent = "";
                var jsonDepartments = new List<JsonDepartment>();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        jsonContent = reader.ReadToEnd();
                        jsonDepartments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonDepartment>>(jsonContent);
                    }
                }

                var departments = new List<Departement>();

                foreach (var jsDepartment in jsonDepartments)
                {
                    var region = Context.Regions.First(r => r.Code == jsDepartment.Region.Code);
                    var department = new Departement
                    {
                        Code = jsDepartment.Code,
                        Region = region,
                        Libelle = jsDepartment.Title,
                    };

                    region.Departements.Add(department);
                    departments.Add(department);
                }

                Context.Departements.AddRange(departments);

                await Context.SaveChangesAsync();
            }
        }

        private async Task InitCitiesAndZipCodes()
        {
            _logger.LogInformation($"Début du seed des codepostaux et des villes");
            if ((await Context.ZipCodes.AnyAsync()) == false)
            {
                var villesToAdd = new List<City>();
                var codesPostauxToAdd = new List<ZipCode>();
                _logger.LogInformation("On Commence le seed des codepostaux et des villes");
                Assembly assembly = typeof(SandBoxDbContext).GetTypeInfo().Assembly;
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains("cities_All.json"));
                _logger.LogInformation($"utilisation du fichier {resourceName}");
                var jsonContent = "";
                var jsonCities = new List<JsonCity>();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        jsonContent = reader.ReadToEnd();
                        jsonCities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonCity>>(jsonContent);
                        _logger.LogInformation($"Nombre de ville dans jsonCities {jsonCities.Count}");
                    }
                }
                var idfrance = await Context.Countries.Where(c => c.Libelle == "France").Select(x => x.Id).FirstOrDefaultAsync();
                var zipCodes = new Dictionary<string, ZipCode>();
                var cities = new List<City>();

                // optimisation pour ne pas récupérer le département à chaque fois
                string currentDepartmentCode = "";
                int? idDepartment = null;
                foreach (var city in jsonCities.Where(x => !string.IsNullOrEmpty(x.Code_postal)).OrderBy(x => x.Code_postal))
                {
                    var codeInsee = city.Code_commune_INSEE;
                    // nom de la ville
                    var cityName = city.Nom_commune;
                    if (string.IsNullOrEmpty(cityName) && !string.IsNullOrEmpty(city.Libelle_acheminement))
                    {
                        cityName = city.Libelle_acheminement;
                    }
                    if (string.IsNullOrEmpty(cityName) && !string.IsNullOrEmpty(city.Libelle_acheminement_2))
                    {
                        cityName = city.Libelle_acheminement_2;
                    }
                    //code postal
                    var codePostal = codesPostauxToAdd.FirstOrDefault(x => x.Libelle == city.Code_postal);
                    if (codePostal == null)
                    {
                        codePostal = new ZipCode
                        {
                            Libelle = city.Code_postal,
                            IdPays = idfrance
                        };
                        codesPostauxToAdd.Add(codePostal);
                    }
                    if (!string.IsNullOrEmpty(cityName))
                    {
                        var newDepartmentCode = codePostal.Libelle.Substring(0, 2);
                        // Cas des outres mer
                        if (codePostal.Libelle.StartsWith("971")
                            || codePostal.Libelle.StartsWith("972")
                            || codePostal.Libelle.StartsWith("973")
                            || codePostal.Libelle.StartsWith("974")
                            || codePostal.Libelle.StartsWith("975")
                            || codePostal.Libelle.StartsWith("976")
                            || codePostal.Libelle.StartsWith("977")
                            || codePostal.Libelle.StartsWith("978")
                            || codePostal.Libelle.StartsWith("979")
                            || codePostal.Libelle.StartsWith("980")
                            || codePostal.Libelle.StartsWith("981")
                            || codePostal.Libelle.StartsWith("982")
                            || codePostal.Libelle.StartsWith("983")
                            || codePostal.Libelle.StartsWith("984")
                            || codePostal.Libelle.StartsWith("985")
                            || codePostal.Libelle.StartsWith("986")
                            || codePostal.Libelle.StartsWith("987")
                            || codePostal.Libelle.StartsWith("988"))
                        {
                            newDepartmentCode = codePostal.Libelle.Substring(0, 3);
                        }
                        if (currentDepartmentCode != newDepartmentCode)
                        {
                            currentDepartmentCode = newDepartmentCode;
                            idDepartment = await Context.Departements.Where(d => d.Code == newDepartmentCode).Select(x => x.Id).FirstOrDefaultAsync();
                        }
                        if (idDepartment.HasValue && idDepartment.Value > 0 && !villesToAdd.Any(x => x.Libelle.ToLower() == cityName.ToLower() && x.CodePostal.Libelle == codePostal.Libelle))
                        {
                            _logger.LogInformation($"Ville ajoutée : {cityName}");
                            _logger.LogInformation($"Avec le code postale : {codePostal.Libelle}");
                            _logger.LogInformation($"Département id : {idDepartment.Value}");
                            var newCity = new City
                            {
                                Libelle = cityName,
                                CodePostal = codePostal,
                                CodeINSEE = codeInsee,
                                IdDepartement = idDepartment.Value,
                                IdPays = idfrance
                            };
                            villesToAdd.Add(newCity);
                        }
                    }
                }
                Context.ZipCodes.AddRange(codesPostauxToAdd);
                await Context.SaveChangesAsync();
                Context.Cities.AddRange(villesToAdd);
                _logger.LogInformation("Sauvegarde des villes et des codes postaux");
                await Context.SaveChangesAsync();
                _logger.LogInformation("Sauvegarde des villes et des codes postaux réussie");
            }
        }
        #endregion

        private async Task SeedData()
        {
            Assembly assembly = typeof(SandBoxDbContext).GetTypeInfo().Assembly;
#if DEBUG
            string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains("temperature.json"));
#else
            string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains("temperature.json"));
#endif
            List<TmaxForRegion> tmaxsForRegion;
            var jsonContent = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    jsonContent = reader.ReadToEnd();
                    tmaxsForRegion = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TmaxForRegion>>(jsonContent);
                }
            }
            foreach (var tmaxForRegion in tmaxsForRegion)
            {
                if (tmaxForRegion.fields != null)
                {
                    var idContry = (await Context.Countries.FirstOrDefaultAsync(x => x.Libelle == "France"))?.Id;
                    var idRegion = (await Context.Regions.FirstOrDefaultAsync(x => x.Code == tmaxForRegion.fields.code_insee_region))?.Id;
                    if (idContry.HasValue && idRegion.HasValue)
                    {
                        var temparture = new Temperatures()
                        {
                            TemperatureMin = tmaxForRegion.fields.tmin,
                            TemperatureMoy = tmaxForRegion.fields.tmoy,
                            TemperatureMax = tmaxForRegion.fields.tmax,
                            IdCountry = idContry.Value,
                            IdRegion = idRegion.Value
                        };
                        Context.Temperatures.Add(temparture);
                    }
                }
            }
            await Context.SaveChangesAsync();
        }
    }
}
