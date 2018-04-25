using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.Model
{
    class ExaminationDetail
    {
        public string description { get; set; }

        public int? spoValue { get; set; }

        public int? temperatureValue { get; set; }

        public int? systolicValue { get; set; }

        public int? diastolicValue { get; set; }

        public int? meanBloodPressure { get; set; }

        public int? pulseRate { get; set; }

        public DateTime? examinationDate { get; set; }
    }
}
