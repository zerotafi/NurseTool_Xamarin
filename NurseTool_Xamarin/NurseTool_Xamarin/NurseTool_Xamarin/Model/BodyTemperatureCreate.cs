using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.Model
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
}
