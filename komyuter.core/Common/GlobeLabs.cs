using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using komyuter.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace komyuter.core.Common
{
    public class GlobeLabs
    {
        public static JToken LocateDevice(string mobile_number, string access_token)
        {
            string lbsBaseUrl = Properties.Settings.Default.globe_lbs_baseurl;
            string lbsResource = Properties.Settings.Default.globe_lbs_resource;
            int accuracy = Properties.Settings.Default.globe_lbs_accuracy;
            string url = lbsResource
                + "?access_token=" + access_token 
                + "&address=" + mobile_number 
                + "&requestedAccuracy=" + accuracy.ToString();

            var client = new RestClient(lbsBaseUrl + "/" + url);
            var request = new RestRequest();
            request.Method = Method.GET;
            request.RequestFormat = DataFormat.Json;
            request.Parameters.Clear();
            request.AddHeader("Host", "devapi.globelabs.com.ph");
            var queryResult = client.Execute(request);
            JToken jsonReturn = JsonConvert.DeserializeObject<JToken>(queryResult.Content);

            return jsonReturn;
        }
    }
}
