using NSService.Common;
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

        void AddExaminationToPatient(int patientId, Examination exam, ExaminationType examType, IExaminationType examData);

        bool Save();

        void DeleteExam(Examination exam);

        void AddPatient(Patient patient);

        bool PatientExistsByExtId(int extId);

        Patient GetPatientByExtID(int extId);

        IExaminationType GetExaminationDetail(int patientId, int examinationId);


    }
}
