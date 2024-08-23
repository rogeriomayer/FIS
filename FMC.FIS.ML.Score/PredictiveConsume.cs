using FMC_FIS_ML_Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FMC_FIS_ML_Score.MLGeneric;

namespace FMC.FIS.ML.Score
{
    public class PredictiveConsume
    {
        public ModelOutput GetPredictive(ModelInput modelInput)
        {
            return MLGeneric.Predict(modelInput);
        }

    }
}
