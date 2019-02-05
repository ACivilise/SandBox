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
        private readonly ILogger<MLController> _logger;

        public MLController(IServiceProvider serviceProvider, ILogger<MLController> logger)
        {
            _MLService = serviceProvider.GetService<IMLService>();
        }

        [HttpGet, Route("[area]/[controller]/[action]/")]
        public async Task<IActionResult> FirstPage()
        {
            var model = new FirstPageModel();
            model.Prediction = await _MLService.TestdeML();
            return View(model);
        }
    }
}