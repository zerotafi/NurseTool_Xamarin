using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.Model
{
    public class Examination
    {
        public int id { get; set; }
        public int patientId { get; set; }
        public string description { get; set; }
        public string examinationType { get; set; }
        public string value { get; set; }
    }
}
