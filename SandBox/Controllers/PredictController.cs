using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SandBox.DTOs.DTOs;

namespace SandBox.Controllers
{
    public class PredictController : Controller
    {
        private readonly PredictionEnginePool<IrisData, IrisPrediction> _predictionEnginePool;

        public PredictController(PredictionEnginePool<IrisData, IrisPrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] IrisData input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            IrisPrediction prediction = _predictionEnginePool.Predict(input);
            return prediction.PredictedLabels;
        }
    }
}