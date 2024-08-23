using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models.BvTelecom;
using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using System;
using System.Collections.Generic;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class SmsBLL : BLL<BvSmsResponse, BvSmsDAO>
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

            if (result.result.Contains("Lote recebido com sucesso"))
                return "OK";
            else
                throw new Exception(result.result);
        }

    }
}
