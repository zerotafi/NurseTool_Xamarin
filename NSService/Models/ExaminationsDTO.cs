using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Models
{
    public class ExaminationsDTO
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

    }
}
