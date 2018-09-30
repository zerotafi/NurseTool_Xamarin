using Newtonsoft.Json;
using NurseTool_Xamarin.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NurseTool_Xamarin.Services
{
    class NSServiceClient
    {
        RestClient client;

        public NSServiceClient()
        {
            client = new RestClient("http://0c14d89f.ngrok.io");
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

        public async Task<bool> PostExamBodyTemp(int patientId, int temperatureValue)
        {
            string requestString = string.Format("api/patients/{0}/examination/", patientId);
            var request = new RestRequest(requestString, Method.POST);

            BodyTemperatureCreate dto = new BodyTemperatureCreate()
            {
              PatientId = patientId,
              TemperatureValue = temperatureValue,
              ExaminationType = "Body temperature",
              Description = "Body temperature",
              Value = DateTime.Now.ToString()
            };
            var body = JsonConvert.SerializeObject(dto);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", body, ParameterType.RequestBody); 


            IRestResponse response = client.Execute(request);
            var content = response.Content;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> PostExamBodySPO(int patientId, int spoValue)
        {
            string requestString = string.Format("api/patients/{0}/examination/", patientId);
            var request = new RestRequest(requestString, Method.POST);

            BodyTemperatureCreate dto = new BodyTemperatureCreate()
            {
                PatientId = patientId,
                SPOValue = spoValue,
                ExaminationType = "SpO2",
                Description = "SpO2",
                Value = DateTime.Now.ToString()
            };
            var body = JsonConvert.SerializeObject(dto);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", body, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);
            var content = response.Content;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> PostExamBodyBloodPressure(int patientId, int diastolicValue, int systolicValue,
            int meanBloodPressure, int pulseRate)
        {
            string requestString = string.Format("api/patients/{0}/examination/", patientId);
            var request = new RestRequest(requestString, Method.POST);

            BodyTemperatureCreate dto = new BodyTemperatureCreate()
            {
                PatientId = patientId,
                SystolicValue = systolicValue,
                DiastolicValue = diastolicValue,
                MeanBloodPressure = meanBloodPressure,
                PulseRate = pulseRate,
                ExaminationType = "Blood Pressure",
                Description = "Blood Pressure",
                Value = DateTime.Now.ToString()
            };
            var body = JsonConvert.SerializeObject(dto);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", body, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);
            var content = response.Content;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> ArchiveExamination(int examinationID)
        {
            string requestString = string.Format("api/HL7Com/send/{0}", examinationID);
            var request = new RestRequest(requestString, Method.GET);
            IRestResponse response = client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public bool AuthUser(User user)
        {
            string requestString = string.Format("/api/auth");
            var request = new RestRequest(requestString, Method.POST);
            var body = JsonConvert.SerializeObject(user);

            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            { return false; }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            { return true; }
            return false;
        }

    }
}
