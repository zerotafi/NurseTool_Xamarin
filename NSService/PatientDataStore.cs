using NSService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService
{
    public class PatientDataStore
    {
        public static PatientDataStore Current { get; } = new PatientDataStore();

        public List<PatientDTO> Patients { get; set; }

        public PatientDataStore()
        {
            Patients = new List<PatientDTO>()
            {
                new PatientDTO()
                {
                    Age = 10,
                    Gender = "Male",
                    Id = 1,
                    Name = "Jhon Doe",
                    Examinations = new List<ExaminationsDTO>()
                    { new ExaminationsDTO(){Id = 1, Description = "BloodPressure", PatientId = 1,  Value = "180,120 80"} }
                },
                new PatientDTO()
                {
                    Age = 22,
                    Gender = "Male",
                    Id = 2,
                    Name = "Jim Patt",
                     Examinations = new List<ExaminationsDTO>()
                    { new ExaminationsDTO(){Id = 2, Description = "BloodPressure", PatientId = 2,  Value = "180,120 80"} ,
                      new ExaminationsDTO(){Id = 3, Description = "Temp", PatientId = 2,  Value = "38" } }
                },
                new PatientDTO()
                {
                    Age = 31,
                    Gender = "Female",
                    Id = 3,
                    Name = "Jane Doe"
                },

                new PatientDTO()
                {
                    Age = 21,
                    Gender = "Male",
                    Id = 4,
                    Name = "Jhon Stevens"
                }
            };
        }
    }
}
