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
            client = new RestClient("http://d5ca4377.ngrok.io");
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

        public async Task<List<WorkFlow>> GetWorkFlow(User user, int patientId)
        {
            string requestString = string.Format("/api/workflow/patient/{0}", patientId);
            var request = new RestRequest(requestString, Method.GET);
            List<WorkFlow> Items = new List<WorkFlow>();
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Items = JsonConvert.DeserializeObject<List<WorkFlow>>(content);

            return Items;

        }

        public async Task<List<WorkFlowStep>> GetWorkFlowSteps(int workFlowID)
        {
            string requestString = string.Format("/api/workflow/{0}/workFlowSteps", workFlowID);
            var request = new RestRequest(requestString, Method.GET);
            List<WorkFlowStep> Items = new List<WorkFlowStep>();
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Items = JsonConvert.DeserializeObject<List<WorkFlowStep>>(content);

            return Items;
        }

        public async Task<WorkFlow> CreateNewWF(User user, int patientId, string wfName)
        {
            string requestString = string.Format("/api/workflow/patient/{0}/userInfo/{1}//WFName/{2}", patientId, user.Username, wfName);
            var request = new RestRequest(requestString, Method.GET);
            WorkFlow Item = new WorkFlow();
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Item = JsonConvert.DeserializeObject<WorkFlow>(content);

            return Item;
        }
        public async Task<bool> AddWorkFlowStepToWorkFlow(int wfID, string WFStepName)
        {
            string requestString = string.Format("/api/workflow/workFlowId/{0}/wfStepName/{1}", wfID, WFStepName);
            var request = new RestRequest(requestString, Method.GET);
            WorkFlow Item = new WorkFlow();
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        } 

    }
}
