﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.REST.Models
{
    public class AccessTokenResponse
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string id_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}
