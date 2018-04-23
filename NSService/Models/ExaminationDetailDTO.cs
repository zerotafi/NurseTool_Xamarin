using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Models
{
    public class ExaminationDetailDTO
    {
        public string Description { get; set; }

        public int? SPOValue { get; set; }

        public int? TemperatureValue { get; set; }

        public int? SystolicValue { get; set; }

        public int? DiastolicValue { get; set; }

        public int? MeanBloodPressure { get; set; }

        public int? PulseRate { get; set; }

        public DateTime? ExaminationDate { get; set; }


    }
}
