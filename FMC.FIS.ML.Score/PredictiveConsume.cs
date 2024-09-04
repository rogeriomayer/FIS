using FMC_FIS_EnvioEmailCredz1;
using static FMC_FIS_EnvioEmailCredz1.MLGeneric;

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
