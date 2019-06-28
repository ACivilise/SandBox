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
using System.Collections.Generic;

namespace SandBox.Services.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IServiceProvider serviceProvider)
        {
            _weatherRepository = serviceProvider.GetService<IWeatherRepository>();
        }


        //The purpose of the output columns in scored IDataView is according to the learning task.e.g. if task is
        //Regression
        //Label: Original regression value of the example.
        //Score: Predicted regression value.
        //Binary Classification
        //Label: Original Label of the example.
        //Score: Raw score from the learner (e.g.value before applying sigmoid function to get probability).
        //Probability: Probability of being in certain class
        //PredictedLabel : Predicted class.
        //Multi-class Classification
        //Label: Original Label of the example.
        //Score: Its an array whose length is equal to number of classes and contains probability for each class.
        //PredictedLabel: Predicted class.
        //Clustering
        //Label: Original cluster Id of the example.
        //Score: Its an array whose length is equal to number of clusters.It contains square distance from the cluster centeriod.
        //PredictedLabel: Predicted cluster Id.
        public async Task TrainModel()
        {
            try
            {
                var data = await _weatherRepository.GetTemperature();
                var mlContext = new MLContext();
                var trainingDataView = mlContext.Data.LoadFromEnumerable(data);
                string featuresColumnName = "Features";
                var pipeline = mlContext.Transforms
                        .Concatenate(featuresColumnName, 
                                            nameof(TemperaturesDTO.Month),
                                            nameof(TemperaturesDTO.Day))
                        .Append(mlContext.Transforms.Conversion.ConvertType(featuresColumnName, outputKind: DataKind.Single))
                        .Append(mlContext.Transforms.Conversion.ConvertType(nameof(TemperaturesDTO.TemperatureMoy), outputKind: DataKind.Single))
                        .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(TemperaturesDTO.TemperatureMoy), 
                                                                    featureColumnName: featuresColumnName,
                                                                    maximumNumberOfIterations: 100));
                // Build machine learning model
                var trainedModel = pipeline.Fit(trainingDataView);
                //var model = pipeline.Fit(trainingDataView);
                var predictor = mlContext.Model.CreatePredictionEngine<TemperaturesDTO, TemperaturesDTO>(trainedModel);
                var inputData = new TemperaturesDTO()
                {
                    Date = DateTime.Now,
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Hour = 0,
                    IdRegion = 1
                };
                var prediction = predictor.Predict(inputData);
                Console.WriteLine($"Cluster: {prediction.TemperatureMoy}");
                SaveModelAsFile(mlContext, trainedModel, trainingDataView);
            }
            catch (Exception exception)
            {

            }
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
