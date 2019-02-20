using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SandBox.Services.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SandBox.Areas.ML.ViewModels;

namespace SandBox.Areas.ML.Controllers
{
    [Area(Constantes.Areas.ML)]
    public class MLController : Controller
    {
        private readonly IMLService _MLService;
        private readonly IGoogleORService _googleORService;
        private readonly ILogger<MLController> _logger;

        public MLController(IServiceProvider serviceProvider, ILogger<MLController> logger)
        {
            _MLService = serviceProvider.GetService<IMLService>();
            _googleORService = serviceProvider.GetService<IGoogleORService>();
        }

        [HttpGet, Route("[area]/[controller]/[action]/")]
        public bool TestGoogleOR()
        {
            _googleORService.RunLinearProgrammingExample("GLOP_LINEAR_PROGRAMMING");
            return true;
        }

        [HttpGet, Route("[area]/[controller]/[action]/")]
        public bool TrainModel()
        {
            var model = new FirstPageModel();
            _MLService.TrainModel();
            return true;
        }

        [HttpGet, Route("[area]/[controller]/[action]/")]
        public IActionResult FirstPage()
        {
            var model = new FirstPageModel();
            model.Prediction =  _MLService.PredictFromFile();
            return PartialView(model);
        }
    }
}