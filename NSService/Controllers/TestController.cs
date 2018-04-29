using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSService.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Controllers
{

    class BodyTemperatureCreate
    {
        public int PatientId { get; set; }

        public string Description { get; set; }

        public string ExaminationType { get; set; }

        public string Value { get; set; }

        public int? SPOValue { get; set; }

        public int? TemperatureValue { get; set; }

        public int? SystolicValue { get; set; }

        public int? DiastolicValue { get; set; }

        public int? MeanBloodPressure { get; set; }

        public int? PulseRate { get; set; }
    }
    public class TestController : Controller
    {
        public PatientInfoContext _ctx;

        public TestController(PatientInfoContext ctx)

        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDDatabase()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/tester")]
        public IActionResult TestTester()
        {

            RestClient client = new RestClient("http://localhost:51606/");
            string requestString = string.Format("api/patients/{0}/examination/", 1);
            var request = new RestRequest(requestString, Method.POST);

            BodyTemperatureCreate dto = new BodyTemperatureCreate()
            {
                PatientId = 1,
                SystolicValue = 5,
                DiastolicValue = 5,
                MeanBloodPressure = 5,
                PulseRate = 5,
                ExaminationType = "Blood Pressure",
                Description = "Blood Pressure",
                Value = DateTime.Now.ToString()
            };


          
            var test = JsonConvert.SerializeObject(dto);
            
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", test, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);
            var content = response.Content;

            return Ok();
        }
    }
}
