using FMC.FIS.Business.Models.BvTelecom;
using FMC.FIS.Business.DAO;
using FMC.Generic;
using System;
using FMC.FIS.Business.Models.FIS;

namespace FMC.FIS.Business.BLL
{
    public class SmsBLL : BLL<SMS, SmsDAO>
    {
        public SMS GetByIdPerson(long idPerson, DateTime dtEnvio)
        {
            return persistence.GetByIdPerson(idPerson, dtEnvio);
        }
    }

    public class BvSmsBLL : BLL<BvSmsResponse, BvSmsDAO>
    {
        /// <summary>
        /// Envia um SMS
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string SmsSingle(SingleRequest single)
        {
            var result = persistence.SmsSingle(single);

            if (result.result.Contains("Message sent successfully"))
                return "OK";
            else
                throw new Exception(result.result);
        }

        public string SmsBulk(BulkRequest bulkMessage)
        {
            var result = persistence.SmsBulk(bulkMessage);

            if (result.result.Contains("Lote recebido com sucesso") || result.result.Contains("Message received successfully"))
                return "OK";
            else
                throw new Exception(result.result);
        }

    }
}
