using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMC.FIS.API.Models.BvTelecom
{
    public class SingleRequest
    {
        [Required(ErrorMessage = "celular é obrigatorio")]
        public string celular { get; set; }

        [Required(ErrorMessage = "mensagem é obrigatorio")]
        public string mensagem { get; set; }

        [Required(ErrorMessage = "parceiroId é obrigatorio")]
        public string parceiroId { get; set; }

        [Required(ErrorMessage = "carteiraId é obrigatorio")]
        public long carteiraId { get; set; }
    }

    public class BulkRequest
    {
        public ICollection<SingleRequest> bulk { get; set; }
    }

    public class SmsResponse
    {
        public string result { get; set; }
    }
}
