
using Microsoft.ML.Data;

namespace SandBox.DTOs.DTOs
{
    public class IrisPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
