using SandBox.DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandBox.Areas.ML.ViewModels
{
    public class FirstPageModel
    {
        public string Name { get; set; } = "FirstPage";

        public ClusterPrediction Prediction  { get; set; }
}
}
