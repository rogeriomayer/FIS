using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Business.DAO;

namespace FMC.FIS.Business.BLL
{
    public class ScoreAiBLL : BLL<ScoreAI, ScoreAiDAO>
    {

        public Decimal GetSingleScore(FMC_FIS_ML_Score.MLGeneric.ModelInput model)
        {
            var output = new FMC.FIS.ML.Score.PredictiveConsume().GetPredictive(model);
            return output != null ? Convert.ToDecimal(output.Score) : decimal.Zero;
        }

        public ICollection<ScoreAI> GetProductScoreByProductType(int count)
        {
            return persistence.GetProductScoreByProductType(count);
        }


    }

}
