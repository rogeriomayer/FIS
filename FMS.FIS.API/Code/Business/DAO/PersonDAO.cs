using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class PersonDAO : AbstractRepositoryPersistence<Person>
    {
        public PersonDAO() : base("CNN_FIS") { }

        public Person GetByCPF(string cpf)
        {
            return Context.Where(p => p.NrCNPJCPF == cpf).FirstOrDefault();
        }
    }
}
