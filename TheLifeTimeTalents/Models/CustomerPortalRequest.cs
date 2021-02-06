using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheLifeTimeTalents.Models
{
    public class CustomerPortalRequest
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}
