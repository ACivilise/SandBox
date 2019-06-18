using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SandBox.DTOs.DTOs;

namespace SandBox.Controllers
{
    public class PredictController : Controller
    {
        private readonly PredictionEnginePool<IrisData, ClusterPrediction> _predictionEnginePool;

        public PredictController(PredictionEnginePool<IrisData, ClusterPrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpPost]
        public ActionResult<float> Post([FromBody] IrisData input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var prediction = _predictionEnginePool.Predict(input);
            return prediction.Distances.First();
        }
    }
}