using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.Model
{
    public class Patient
    {
        public int id { get; set; }

        public string name { get; set; }

        public int age { get; set; }

        public string gender { get; set; }

        public int ExternalId { get; set; }


        // add to json result.

        //public string OriginalHL7Message { get; set; }

        //public DateTime BirthDate { get; set; }

        //public string Address { get; set; }

        // public ICollection<ExaminationsDTO> Examinations { get; set; } = new List<ExaminationsDTO>();

        // public int NumberOfExaminations { get { return Examinations.Count; } }
    }
}
