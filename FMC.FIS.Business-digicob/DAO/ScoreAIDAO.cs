using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.FIS.Business.DAO
{
    public class ScoreAiDAO : AbstractRepositoryPersistence<ScoreAI>
    {
        public ScoreAiDAO() : base("CNN_FIS") { }

        public ICollection<ScoreAI> GetProductScoreByProductType(int count)
        {
            //return Context.Where(p => p.IdProductType == productType).OrderByDescending(p => p.IdProduct).ToList();
            var query = new StringBuilder();
            query.Append(" select TOP ").Append(count).Append(" * ");
            query.Append(" from ScoreAI ");
            query.Append(" where DtUpdate <= CONVERT(Date,getdate() -8) ");
            query.Append(" ORDER BY SCORE  DESC ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }

    }
}
