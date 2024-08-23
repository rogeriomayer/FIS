using System.Collections.Generic;

namespace FMC.FIS.API.Models.Customer
{
    public class PersonUpdateRequest
    {
        public PersonUpdateRequest()
        {
            PhoneUpdateRequest = new HashSet<PhoneUpdateRequest>();
        }

        public long IdPerson { get; set; }

        public string Email { get; set; }

        public int IdUserLogin { get; set; }
        public ICollection<PhoneUpdateRequest> PhoneUpdateRequest { get; set; }
        public AddressUpdateRequest AddressUpdateRequest { get; set; }
    }

    public class AddressUpdateRequest
    {
        /// <summary>
        /// Id do endereço
        /// </summary>
        public long IdAddress { get; set; }
        /// <summary>
        /// CEP
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Numero
        /// </summary>
        public string NrAddress { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string Complement { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string UF { get; set; }

    }

    public class PhoneUpdateRequest
    {
        /// <summary>
        /// Id do telefone
        /// </summary>
        public long IdPhone { get; set; }

        public string PhoneType { get; set; }

        public int IdPhoneStatus { get; set; }

        /// <summary>
        /// Numero do telefone
        /// </summary>
        public string NrPhone { get; set; }

        /// <summary>
        /// Deletar telefone
        /// </summary>
        public bool Remove { get; set; }

        /// <summary>
        /// Atualizar telefone
        /// </summary>
        public bool Update { get; set; }

    }
}
