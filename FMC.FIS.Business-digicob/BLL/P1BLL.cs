using FMC.FIS.Business.Models.Boleto;
using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.BLL
{
    public class P1BLL
    {
        public CardsP1Response GetCards(string cpf)
        {
            try
            {
                string cpfRequest = "{\"nrCpfCnpj\" :\"" + cpf + "\"}";
                var headers = new Dictionary<string, string>();
                headers.Add("x-authorization", Constants.TOKEN_API_P1);
                return RestApi.Post<CardsP1Response, string>(Constants.URL_API_P1, "ConsultaCartaoNomeCpfCnpj/V1", cpfRequest, headers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CardP1Response GetCard(string tokenCard)
        {
            try
            {
                string cpfRequest = "{\"nrCartao\" :\"" + tokenCard + "\"}";
                var headers = new Dictionary<string, string>();
                headers.Add("x-authorization", Constants.TOKEN_API_P1);
                return RestApi.Post<CardP1Response, string>(Constants.URL_API_P1, "ConsultaInformacoesCartao/V1", cpfRequest, headers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
