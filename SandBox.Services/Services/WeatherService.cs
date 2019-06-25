using Microsoft.ML;
using Microsoft.ML.Data;
using SandBox.DataAccess.Repositories.ML.Interfaces;
using SandBox.DTOs.DTOs;
using SandBox.Services.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using SandBox.DTOs.DTOs.Weather;
using SandBox.DataAccess.Repositories.Weather.Interfaces;

namespace SandBox.Services.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IServiceProvider serviceProvider)
        {
            _weatherRepository = serviceProvider.GetService<IWeatherRepository>(); 
        }

        public async Task TrainModel()
        {
            var data = await _weatherRepository.GetTemperature();
        }

        private static void SaveModelAsFile(MLContext mlContext, ITransformer model, IDataView trainingDataView)
        {           

        }

        public TemperaturesPrediction PredictFromFile()
        {
            //var mlContext = new MLContext();
            //var repertoireSortie = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //var _modelPath = repertoireSortie + "/model.save";
            ////Define DataViewSchema for data preparation pipeline and trained model
            //DataViewSchema modelSchema;
            //// Load trained model
            //ITransformer trainedModel = mlContext.Model.Load(_modelPath, out modelSchema);
            //var reader = mlContext.Data.CreateTextLoader<IrisData>(separatorChar: ',', hasHeader: false);
            //var trainingDataView = reader.Load($"{repertoireSortie}/DataSources/iris.data");
            //// STEP 4: Train your model based on the data set  
            //var predictor = mlContext.Model.CreatePredictionEngine<TemperaturesDTO, TemperaturesPrediction>(trainedModel);
            //var inputData = new TemperaturesDTO()
            //{
            //   Date = DateTime.Now
            //};
            //var prediction = predictor.Predict(inputData);
            //Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            //Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
            return new TemperaturesPrediction();
        }
    }
}
