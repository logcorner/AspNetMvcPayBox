using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logcorner.Models
{
    public class PayBoxModel
    {
        public PayBoxModel()
        {
        }
            public string PBX_SITE { get; set; }
            public string PBX_RANG { get; set; }
            public string PBX_IDENTIFIANT { get; set; }
            public string PBX_TOTAL { get; set; }
            public string PBX_DEVISE { get; set; }
            public string PBX_CMD { get; set; }
            public string PBX_PORTEUR { get; set; }
            public string PBX_RETOUR { get; set; }
            public string PBX_HASH { get; set; }
            public string PBX_TIME { get; set; }
            public string PBX_TYPEPAIEMENT { get; set; }
            public string PBX_TYPECARTE { get; set; }
            public string PBX_EFFECTUE { get; set; }
            public string PBX_REFUSE { get; set; }
            public string PBX_ANNULE { get; set; }
            public string PBX_ERREUR { get; set; }
            public string PBX_HMAC { get; set; }
     }
}