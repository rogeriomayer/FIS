using FMC.Digicob.DM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FMC.FIS.Business.Code.Api.Digicob
{
    public class DigicobAPI
    {
        private readonly HttpClient _http;

        public DigicobAPI()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://10.40.0.52/digicob/")
            };

            _http.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ICollection<ContractResponse>> GetContractAsync(string cpf, string dtBirth)
        {
            try
            {
                var url = string.IsNullOrWhiteSpace(dtBirth) ? $"api/contract/{cpf}/DM" : $"api/contract/{cpf}/DM/{dtBirth}";
                var response = await _http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<ICollection<ContractResponse>>();
                else
                    return new List<ContractResponse>();
            }
            catch(Exception ex)
            {
                return new List<ContractResponse>();
            }
        }

        public async Task<ICollection<string>> GetListNames(string name, int count)
        {
            var response = await _http.GetAsync($"api/customer/names/{name}/{count}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ICollection<string>>();
            else
                return new List<string>();
        }

        public async Task<ICollection<AgreementPlanResponse>> PostAgreementPlansAsync(long customerId, AgreementPlanRequest agreementPlanRequest)
        {
            var response = await _http.PostAsJsonAsync($"api/agreement/plans/{customerId}", agreementPlanRequest);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ICollection<AgreementPlanResponse>>();
            else
                return new List<AgreementPlanResponse>();
        }

        public async Task<AgreementResponse> GetAgreementAsync(long customerId, string planUuid)
        {
            var response = await _http.GetAsync($"api/agreement/DM/{customerId}/{planUuid}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AgreementResponse>();
            else
                return null;
        }

        public async Task<AgreementResponse> GetAgreementBilletAsync(long customerId, long agreementId, long installmentId)
        {
            var response = await _http.GetAsync($"api/agreement/billet/{customerId}/{agreementId}/{installmentId}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AgreementResponse>();
            else
                return null;
        }

        public async Task<bool> PostSendSmsAsync(string phone, string message)
        {
            var payload = new { Phone = phone, Message = message, Origem = "DM" };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/contact/sms", content);

            if (response.IsSuccessStatusCode)
                return true;
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return false;
            }
        }

        public async Task<bool> PostSendEmailAsync(string subject, string body, string from, IList<string> to, string smtpName, string userSMTP, string urlAttachment, string attachmentName)
        {
            var payload = new { Subject = subject, Body = body, From = from, To = to, SmtpName = smtpName, UserSMTP = userSMTP, UrlAttachment = urlAttachment, AttachmentName = attachmentName };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/contact/email", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return false;
            }

        }
    }
}
