using SandBox.DTOs.DTOs;
using SandBox.DTOs.DTOs.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SandBox.Services.Services.Interfaces
{
    public interface IWeatherService
    {
        Task TrainModel();

        TemperaturesPrediction PredictFromFile();
    }
}
