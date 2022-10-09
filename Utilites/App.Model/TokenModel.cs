using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANSANG.Utilites.App.Model
{
    public class TokenModel
    {
        public DateTime ExprieDate { get; set; }
        public string Token { get; set; }

        public TokenModel(string json)
        {
            JObject jObject = JObject.Parse(json);
            ExprieDate = (DateTime)jObject["expire"];
            Token = (string)jObject["token"];
        }
    }
}
