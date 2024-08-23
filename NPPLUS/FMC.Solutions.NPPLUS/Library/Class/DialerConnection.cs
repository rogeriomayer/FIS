using DevExpress.XtraEditors;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using FMC.Solutions.NPPLUS.Library.APIDialer;
using FMC.Solutions.NPPLUS.Library.REST;
using System.Threading.Tasks;
using FMC.Solutions.NPPLUS.Library.REST.Models;

namespace FMC.Solutions.NPPLUS.Library.Class
{
    public class DialerConnection
    {

        private static string token;

        private static string WebApiURL = "";

        public static bool LogadoDialer;

        public static string GetToken
        {
            get
            {
                //if (string.IsNullOrEmpty(token))
                //    token = DialerAPI.GetToken(GetCurrentIP);

                return token;
            }
        }


        public static IList<Campanha> GetCampanhas(string agente)
        {
            var listCampanhas = new List<Campanha>();
            // listCampanhas = DialerAPI.GetCampanhas(agente, token);
            return JsonConvert.DeserializeObject<IList<Campanha>>(JsonConvert.SerializeObject(listCampanhas));
        }


        public static IList<CanalInbound> GetCanaisInbound(string campanha)
        {
            var canaisInb = new List<CanalInbound>();
            // canaisInb = DialerAPI.GetCanaisInbound(campanha, token);
            return JsonConvert.DeserializeObject<IList<CanalInbound>>(JsonConvert.SerializeObject(canaisInb));
        }

        public static bool LoginDialer(string agente, string senha, string ramal)
        {
            var login = new Library.APIDialer.Login()
            {
                Usuario = agente,
                Senha = senha,
                Ramal = ramal
            };

            LogadoDialer = AytyAPI.Login(agente) == 1;


            return LogadoDialer;
        }

        //public static Task<AytyGetMailingAPIResponse> MailingApi;

        private static Bilhete Bilhete = new Bilhete();
        private static AytyGetMailingAPIResponse MailingApi = new AytyGetMailingAPIResponse();
        public static AytyAgentAPIResponse AytyDialerAgentAPI = new AytyAgentAPIResponse();

        public static Bilhete GetBilhete(string agente)
        {
            AytyDialerAgentAPI = AytyAPI.GetAgentAPI(agente);

            Bilhete.Status = GetStatus(AytyDialerAgentAPI.DeReturnAPI);
            if (Bilhete.Status == Status.Pausa)
                Bilhete.IdPausa = AytyDialerAgentAPI.IdReturnAPI;
            else
                Bilhete.IdPausa = 0;

            Bilhete.TipoChamada = TipoChamada.Aguardando;

            //if (MailingApi != null && MailingApi.IsCompleted && (MailingApi.Exception == null))
            if (Bilhete.Status == Status.EmChamada)
            {
                if (string.IsNullOrEmpty(Bilhete.IdCall) || Bilhete.IdCall == "0")
                {
                    MailingApi = AytyAPI.GetMailingAPI(agente, "");
                }
                SetBilhete(agente);
            }
            if (Bilhete.Status == Status.Deslogado)
                Bilhete.DescricaoStatus = "Deslogado";
            else
            {
                string statusIPBX = GetStatusIPBX(agente);
                Bilhete.DescricaoStatus = AytyDialerAgentAPI.DeReturnAPI + (string.IsNullOrWhiteSpace(statusIPBX) ? "" : " - " + statusIPBX);
            }
            return Bilhete;
        }

        private static void SetBilhete(string agent)
        {
            Bilhete.IdCall = MailingApi.IdCall.ToString();
            Bilhete.Conta = string.IsNullOrEmpty(MailingApi.CodeMailingClient) ? "409281******9042" : MailingApi.CodeMailingClient;
            Bilhete.Campanha = MailingApi.NmCampaign;
            Bilhete.CPF = string.IsNullOrEmpty(MailingApi.NuRegistration) || MailingApi.NuRegistration.Contains("000000") ? "36950570503" : MailingApi.NuRegistration;
            Bilhete.DescricaoStatus = "Chamada em andamento" + GetStatusIPBX(agent);
            Bilhete.NumeroTelefone = MailingApi.NuDDD + MailingApi.NuPhone;
            Bilhete.TipoChamada = GetTipoChamada(AytyDialerAgentAPI.DeReturnAPI);
        }

        private static string GetStatusIPBX(string agent)
        {
            var ret = AytyAPI.GetCodeStatusIPBXFromAgentRealtimeControl(agent);
            return StatusIPBX.Where(p => p.Key == ret.IdReturnAPI).FirstOrDefault().Value;
        }

        private static TipoChamada GetTipoChamada(string tipoChamada)
        {
            if (tipoChamada.ToUpper() == "PREDICTIVE_CALL")
                return TipoChamada.Outbound;
            else if (tipoChamada.ToUpper() == "INBOUND_CALL")
                return TipoChamada.Inbound;
            else if (tipoChamada.ToUpper() == "PROGRESSIVE_CALL")
                return TipoChamada.Manual;
            else
                return TipoChamada.Aguardando;
        }

        private static Status GetStatus(string status)
        {
            if (status.ToUpper() == "LOGIN")
                return Status.Pausa;
            else if (status.ToUpper() == "AVAILABLE")
                return Status.Liberado;
            else if (status.ToUpper() == "DIALING")
                return Status.EmFilaEspera;
            else if (status.ToUpper() == "RINGING")
                return Status.EmFilaEspera;
            else if (status.ToUpper() == "LOGOFF")
                return Status.Deslogado;
            else if (status.ToUpper() == "OTHER")
                return Status.Deslogado;
            else if (status.ToUpper() == "PAUSE")
                return Status.Pausa;
            else if (status.ToUpper() == "PAUSE_REDIAL")
                return Status.Pausa;
            else if (status.ToUpper() == "PREDICTIVE_CALL")
                return Status.EmChamada;
            else if (status.ToUpper() == "PROGRESSIVE_CALL")
                return Status.EmChamada;
            else if (status.ToUpper() == "INBOUND_CALL")
                return Status.EmChamada;
            else if (status.ToUpper() == "ACW")
                return Status.Pausa;
            else
                return Status.Deslogado;
        }

        public static bool Libera(string agent)
        {
            try
            {
                if (AytyDialerAgentAPI.DeReturnAPI == "ACW")
                    try
                    {
                        FinalizarChamada(agent, Bilhete.IdCall, "211", true);
                    }
                    catch (Exception ex) { }
                if (AytyDialerAgentAPI.IdStatusPauseSchedule <= 0)
                    return AytyAPI.SetAvailable(agent) == 1;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EnviarPausa(string agent, string idStatus)
        {
            try
            {
                var ret = AytyAPI.StartPause(agent, idStatus);
                return ret.IdReturnAPI == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool TransferirChamada(string channel, string token)
        {
            return true;
        }

        public static bool FinalizarChamada(string agent, string idCall, string idStatus, bool finalizarAtendimento)
        {
            try
            {
                var ret = AytyAPI.FinishCall(agent, idCall, idStatus, finalizarAtendimento, "", "", "");
                Bilhete = new Bilhete();
                return ret.IdReturnAPI == 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ICollection<Pausa> ListaPausa()
        {
            //var pausas = DialerAPI.GetListaPausa(token);
            var pausas = new HashSet<Pausa>();
            return JsonConvert.DeserializeObject<ICollection<Pausa>>(JsonConvert.SerializeObject(pausas));

        }

        public static bool ChamadaManual(string agente, string ddd, string telefone)
        {
            if (AytyDialerAgentAPI.DeReturnAPI == "ACW" || !RealizarChamaManual(agente, ddd, telefone))
            {
                return Redial(agente, ddd, telefone);
            }
            return true;
        }

        public static bool Redial(string agente, string ddd, string telefone)
        {
            try
            {
                FinalizarChamada(agente, Bilhete.IdCall, "211", false);
            }
            catch (Exception ex) { }

            MailingApi = AytyAPI.MakeCallRedial(agente, ddd, telefone);

            if (MailingApi.IdReturnAPI == 1)
            {
                SetBilhete(agente);
                return true;
            }
            else if (MailingApi.DeReturnAPI == "NOT_ALLOW_MAKE_CALL_REDIAL")
            {
                return RealizarChamaManual(agente, ddd, telefone);
            }
            else
                return false;
        }

        private static bool RealizarChamaManual(string agente, string ddd, string phoneNumber)
        {
            MailingApi = AytyAPI.ManualCall(agente, ddd, phoneNumber);
            if (MailingApi.IdReturnAPI == 1)
            {
                SetBilhete(agente);
                return true;
            }
            else
                return false;
        }

        public static void Logout(string agent)
        {
            AytyAPI.Logout(agent);
        }

        private static string GetCurrentIP
        {
            get
            {
                string nome = Dns.GetHostName();

                IPAddress[] ip = Dns.GetHostAddresses(nome);

                return ip[1].ToString();
            }
        }

        private static Dictionary<int, string> StatusIPBX
        {
            get
            {
                return new Dictionary<int, string>
                {
                    {0, "Não atende" },
                    {1, "Telefone inexistente (mensagem da companhia telefônica)" },
                    {2, "Telefone inexistente (mensagem da companhia telefônica)" },
                    {3, "Telefone inexistente (mensagem da companhia telefônica)" },
                    {4, "Telefone indisponível" },
                    {5, "Telefone inexistente (mensagem da companhia telefônica)" },
                    {6, "Telefone indisponível" },
                    {7, "Telefone indisponível" },
                    {8, "Telefone indisponível" },
                    {9, "Telefone indisponível" },
                    {17,"Ocupado" },
                    {18,"Falha de conexão" },
                    {19,"Telefone indisponível" },
                    {20,"Telefone indisponível" },
                    {21,"Bloqueado na telefonia" },
                    {22,"Telefone indisponível" },
                    {23,"Telefone indisponível" },
                    {25,"Telefone indisponível" },
                    {26,"Telefone inexistente (mensagem da companhia telefônica)" },
                    {27,"Telefone inexistente (mensagem da companhia telefônica)" },
                    {28,"Telefone inexistente (mensagem da companhia telefônica)" },
                    {29,"Telefone indisponível" },
                    {30,"Telefone indisponível" },
                    {31,"Telefone indisponível" },
                    {34,"Congestionamento na companhia telefônica" },
                    {38,"Congestionamento na companhia telefônica" },
                    {39,"Telefone indisponível" },
                    {40,"Telefone indisponível" },
                    {41,"Congestionamento na companhia telefônica" },
                    {42,"Congestionamento na companhia telefônica" },
                    {43,"Congestionamento na companhia telefônica" },
                    {44,"Congestionamento na companhia telefônica" },
                    {45,"Congestionamento na companhia telefônica" },
                    {46,"Telefone indisponível" },
                    {47,"Congestionamento na companhia telefônica" },
                    {49,"Congestionamento na companhia telefônica" },
                    {50,"Congestionamento na companhia telefônica" },
                    {51,"Congestionamento na companhia telefônica" },
                    {52,"Congestionamento na companhia telefônica" },
                    {53,"Bloqueado na telefonia" },
                    {54,"Congestionamento na companhia telefônica" },
                    {55,"Bloqueado na telefonia" },
                    {57,"Bloqueado na telefonia" },
                    {58,"Falha de conexão" },
                    {62,"Bloqueado na telefonia" },
                    {63,"Congestionamento na companhia telefônica" },
                    {65,"Falha de conexão" },
                    {66,"Congestionamento na companhia telefônica" },
                    {69,"Congestionamento na companhia telefônica" },
                    {70,"Falha de conexão" },
                    {79,"Telefone indisponível" },
                    {81,"Telefone indisponível" },
                    {82,"Telefone indisponível" },
                    {83,"Telefone indisponível" },
                    {84,"Telefone indisponível" },
                    {85,"Telefone indisponível" },
                    {86,"Falha de conexão" },
                    {87,"Bloqueado na telefonia" },
                    {88,"Congestionamento na companhia telefônica" },
                    {90,"Telefone indisponível" },
                    {91,"Telefone inexistente (mensagem da companhia telefônica)" },
                    {95,"Telefone indisponível" },
                    {96,"Telefone indisponível" },
                    {97,"Telefone indisponível" },
                    {98,"Telefone indisponível" },
                    {99,"Telefone indisponível" },
                    {100,"Telefone indisponível" },
                    {101,"Telefone indisponível" },
                    {102,"Congestionamento na companhia telefônica" },
                    {103,"Telefone indisponível" },
                    {110,"Telefone indisponível" },
                    {111,"Falha de conexão" },
                    {127,"Falha de conexão" },
                    {131,"Secretária eletrônica" },
                    {132,"Drop" },
                    {133,"Communication fail with dialer" },
                    {134,"RECORD ERROR" },
                    {135,"Internal congestion" },
                    {136,"No route available" },
                    {140,"NÃO MAPEADO" },
                    {141,"NÃO MAPEADO" },
                    {142,"NÃO MAPEADO" },
                    {143,"Não atende - Preditivo" },
                    {144,"IVR - Sem porta disponível" },
                    {145,"NÃO MAPEADO" },
                    {146,"IVR - Out Of Service" },
                    {147,"IVR - Call Hangup by Customer" },
                    {148,"IVR - Digital Answer" },
                    {149,"IVR - Call Hangup by IVR" },
                    {150,"Mensagem da operadora - Carrier Preditivo" },
                    {151,"Linha muda - Preditivo" },
                    {152,"Caixa Postal - Preditivo" },
                    {153,"Desistência" },
                    {154,"Falha de comunicação - PA" },
                    {155,"Caixa Postal - Chamada Recusada Pelo Cliente" },
                    {156,"NÃO MAPEADO" },
                    {200,"Chamada Atendida" }
                };
            }
        }

    }
}