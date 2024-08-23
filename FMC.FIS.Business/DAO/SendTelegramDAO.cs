using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Business.Models.Telegram;
using FMC.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class SendTelegramDAO : AbstractRepositoryPersistence<TelegramMessageResponse>
    {
        public SendTelegramDAO() : base("CNN_FIS") { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TelegramMessageResponse>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.Property(v => v.result).HasColumnName("result");
                    });
        }
        public TelegramMessageResponse SingleMessage(string url)
        {
            SqlParameter urlParam = new SqlParameter("@url", SqlDbType.VarChar);

            urlParam.Value = url;
            
            var result = Context.FromSqlRaw("exec Telegram_Text_Message @url ", urlParam).AsEnumerable();

            return result.FirstOrDefault();
        }

    }
}
