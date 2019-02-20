using Microsoft.ML;
using Microsoft.ML.Data;
using SandBox.DataAccess.Repositories.ML.Interfaces;
using SandBox.DTOs.DTOs;
using SandBox.Services.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Microsoft.ML.Core.Data;
using Microsoft.Data.DataView;

namespace SandBox.Services.Services
{
    public class MLService : IMLService
    {
        private readonly IIrisRepository _irisRepository;

        public MLService(IServiceProvider serviceProvider)
        {
            _irisRepository = serviceProvider.GetService<IIrisRepository>(); 
        }

        public void TrainModel()
        {
            // STEP 2: Create a ML.NET environment  
            var mlContext = new MLContext();
            // If working in Visual Studio, make sure the 'Copy to Output Directory'
            // property of iris-data.txt is set to 'Copy always'
            var reader = mlContext.Data.CreateTextLoader<IrisData>(separatorChar: ',', hasHeader: false);
            var repertoireSortie = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            IDataView trainingDataView = reader.Read($"{repertoireSortie}/DataSources/iris.data");
            // STEP 3: Transform your data and add a learner
            // Assign numeric values to text in the "Label" column, because only
            // numbers can be processed during model training.
            // Add a learning algorithm to the pipeline. e.g.(What type of iris is this?)
            // Convert the Label back into original text (after converting to number in step 3)
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"))
                .Append(mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(labelColumn: "Label", featureColumn: "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // STEP 4: Train your model based on the data set  
            var model = pipeline.Fit(trainingDataView);
            SaveModelAsFile(mlContext, model);
        }

        private static void SaveModelAsFile(MLContext mlContext, ITransformer model)
        {
            var repertoireSortie = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var _modelPath = repertoireSortie + "/model.save";
            Console.WriteLine("The model is saved to {0}", _modelPath);
            using (var fs = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, fs);
            }
        }

        public IrisPrediction PredictFromFile()
        {
            var mlContext = new MLContext();
            ITransformer loadedModel;
            var repertoireSortie = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var _modelPath = repertoireSortie + "/model.save";
            using (var stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                loadedModel = mlContext.Model.Load(stream);
            }
            return loadedModel.CreatePredictionEngine<IrisData, IrisPrediction>(mlContext).Predict(
                new IrisData()
                {
                    SepalLength = 3.3f,
                    SepalWidth = 1.6f,
                    PetalLength = 0.2f,
                    PetalWidth = 5.1f,
                });
        }
    }
}
