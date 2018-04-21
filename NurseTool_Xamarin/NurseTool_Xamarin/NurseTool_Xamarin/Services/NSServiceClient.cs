using Newtonsoft.Json;
using NurseTool_Xamarin.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NurseTool_Xamarin.Services
{
     class NSServiceClient
    {
        RestClient client;

        public NSServiceClient()
        {
            client = new RestClient("http://94dd2db7.ngrok.io");
        }

        public async Task<List<Patient>> GetPatients()
        {
            var request = new RestRequest("api/patients", Method.GET);
            List<Patient> Items = new List<Patient>();

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Items = JsonConvert.DeserializeObject<List<Patient>>(content);
       
            return Items;
        }



    }
}
