using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANSANG.Utilites.App.Model
{
    public class TrackModel
    {
        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_description")]
        public string StatusDescription { get; set; }

        [JsonProperty("status_date")]
        public string StatusDate { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonProperty("delivery_description")]
        public string DeliveryDescription { get; set; }

        [JsonProperty("delivery_datetime")]
        public string DeliveryDate { get; set; }

        [JsonProperty("receiver_name")]
        public string ReceiverName { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
