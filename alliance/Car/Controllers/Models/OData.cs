using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car.Controllers.Models
{
    public class OData
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }
        public List<Value> Value { get; set; }
    }
}