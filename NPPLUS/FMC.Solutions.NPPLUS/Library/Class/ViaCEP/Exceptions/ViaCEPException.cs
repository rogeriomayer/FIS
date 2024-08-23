using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class.ViaCEP.Exceptions
{
    public class ViaCEPException : Exception
    {
        public ViaCEPException()
        {
        }

        public ViaCEPException(string message)
            : base(message)
        {
        }

        public ViaCEPException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
