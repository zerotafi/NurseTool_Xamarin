using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Models
{
    public class PatientDTO
    {
        public int Id { get; set; }

        public int ExternalId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string OriginalHL7Message { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public ICollection<ExaminationsDTO> Examinations { get; set; } = new List<ExaminationsDTO>();

        public int NumberOfExaminations { get { return Examinations.Count; } }
    }
}
