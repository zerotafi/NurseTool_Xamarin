﻿using Newtonsoft.Json;
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
            client = new RestClient("http://7372c90e.ngrok.io");
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

        public async Task<List<Examination>> GetExamList(int patientId)
        {
            string requestString = string.Format("api/patients/{0}/examination/", patientId);
            var request = new RestRequest(requestString, Method.GET);
            List<Examination> Items = new List<Examination>();

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Items = JsonConvert.DeserializeObject<List<Examination>>(content);

            return Items;
        }


        public async Task<ExaminationDetail> GetExamDetail(int patientId, int examinationId)
        {
            string requestString = string.Format("api/patients/{0}/examination/{1}", patientId, examinationId);
            var request = new RestRequest(requestString, Method.GET);
            ExaminationDetail Item = new ExaminationDetail();

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Item = JsonConvert.DeserializeObject<ExaminationDetail>(content);

            return Item;
        }


    }
}
