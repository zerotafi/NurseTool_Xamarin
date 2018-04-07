using NSService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Services
{
    public interface IPatientInfoRepository
    {
        bool PatientExists(int patientId);

        IEnumerable<Patient> GetPatients();

        Patient GetPatient(int patientId, bool includeExaminations = false);

        IEnumerable<Examination> GetExaminations(int patientId);

        Examination GetExamination(int patientId, int examinationId);
    }
}
