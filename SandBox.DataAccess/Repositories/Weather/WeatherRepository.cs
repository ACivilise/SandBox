using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SandBox.DataAccess.DBContext;
using SandBox.DataAccess.Repositories.ML.Interfaces;
using SandBox.DataAccess.Repositories.Weather.Interfaces;
using SandBox.DTOs.DTOs.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SandBox.DataAccess.Repositories.Weather
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly SandBoxDbContext Context;
        private readonly AutoMapper.IConfigurationProvider AutomapperProvider;

        public WeatherRepository(SandBoxDbContext context, AutoMapper.IConfigurationProvider automapperProvider)
        {
            Context = context;
            AutomapperProvider = automapperProvider;
        }

        public async Task<List<TemperaturesDTO>> GetTemperature()
        {
            var results = await Context.Temperatures.ProjectTo<TemperaturesDTO>(AutomapperProvider).ToListAsync();
            return results;
        }
    }
}
