using FMC.CREDZ.API.Models;
using FMC.Generic;

namespace FMC.CREDZ.DAO.Persistence
{
    public class AddressDAO : AbstractRepositoryPersistence<Address>
    {
        public AddressDAO() : base("CNN_CREDZ")
        {

        }
    }
}
