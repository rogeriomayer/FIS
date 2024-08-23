using FMC.FIS.API.Models.BvTelecom;
using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class SmsDAO : AbstractRepositoryPersistence<SMS>
    {
        public SmsDAO() : base("CNN_FIS") { }


    }

    public class BvSmsDAO : AbstractRepositoryPersistence<BvSmsResponse>
    {
        public BvSmsDAO() : base("CNN_FIS") { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BvSmsResponse>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.Property(v => v.result).HasColumnName("result");
                    });
        }

        public BvSmsResponse SmsSingle(SingleRequest single)
        {
            SqlParameter telefone = new SqlParameter("@Telefone", SqlDbType.VarChar);
            SqlParameter mensagem = new SqlParameter("@Mensagem", SqlDbType.VarChar);
            SqlParameter carteiraId = new SqlParameter("@CarteiraId", SqlDbType.BigInt);
            SqlParameter parceiroId = new SqlParameter("@ParceiroId", SqlDbType.VarChar);

            telefone.Value = single.celular;
            mensagem.Value = single.mensagem;
            carteiraId.Value = single.carteiraId;
            parceiroId.Value = single.parceiroId;

            var result = Context.FromSqlRaw("exec BV_SMS_SINGLE @Telefone, @Mensagem, @CarteiraId, @ParceiroId ", telefone, mensagem, carteiraId, parceiroId).AsEnumerable();

            return result.FirstOrDefault();
        }

        public BvSmsResponse SmsBulk(BulkRequest bulkMessage)
        {
            if (bulkMessage.bulk == null || bulkMessage.bulk.Count == 0)
                throw new Exception("A lista de mensagens está vazia!");

            if (bulkMessage.bulk.Count > 1000)
                throw new Exception("É aceito o máximo de 1000 mensagens!");



            SqlParameter mensagens = new SqlParameter("@Mensagens", SqlDbType.VarChar);

            mensagens.Value = Newtonsoft.Json.JsonConvert.SerializeObject(bulkMessage.bulk);

            var result = Context.FromSqlRaw("exec BV_SMS_BULK @Mensagens ", mensagens).AsEnumerable();

            return result.FirstOrDefault();
        }
    }


}
