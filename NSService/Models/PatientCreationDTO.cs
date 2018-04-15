using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Models
{
    public class PatientCreationDTO
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public int ExternalId { get; set; }

        public string Address { get; set; }
    }
}
