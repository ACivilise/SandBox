using SandBox.DTOs.DTOs.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SandBox.DataAccess.Repositories.Weather.Interfaces
{
    public interface IWeatherRepository
    {
        Task<List<TemperaturesDTO>> GetTemperature();
    }
}
