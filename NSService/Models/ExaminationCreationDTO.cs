using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Models
{
    public class ExaminationCreationDTO
    {
        [Required(ErrorMessage ="PatientId is not vaild!")]
        public int PatientId { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string ExaminationType { get; set; }

        public string Value { get; set; }
    }
}
