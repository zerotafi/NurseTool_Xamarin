using NSService.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class BloodPressureData: IExaminationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ExaminationId { get; set; }

        [ForeignKey("ExaminationId")]
        public Examination Examination { get; set; }

        public int SystolicValue { get; set; }

        public int DiastolicValue { get; set; }

        public int MeanBloodPressure { get; set; }

        public int PulseRate { get; set; }

        public DateTime ExaminationDate { get; set; }


    }
}
