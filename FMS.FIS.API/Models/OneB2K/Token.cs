﻿namespace FMC.FIS.API.Models.OneB2K
{
    public class Token
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
