﻿using System;
using System.Collections.Generic;

namespace Paypal.Models
{
    public class AccessTokenFormate
    {
        public ClientMetadata  clientMetadata { get; set; }
        public Root root { get; set; }
        public class ClientMetadata
        {
            public string name { get; set; }
            public string display_name { get; set; }
            public string logo_uri { get; set; }
            public List<string> scopes { get; set; }
            public string ui_type { get; set; }
        }

        public class Root
        {
            public string scope { get; set; }
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string app_id { get; set; }
            public int expires_in { get; set; }
            public List<string> supported_authn_schemes { get; set; }
            public string nonce { get; set; }
            public ClientMetadata client_metadata { get; set; }
        }

    }
}
