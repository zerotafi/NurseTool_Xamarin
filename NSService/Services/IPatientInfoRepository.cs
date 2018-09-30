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

        bool Auth(string username, string password);

        IEnumerable<Patient> GetPatients();

        Patient GetPatient(int patientId, bool includeExaminations = false);

        WorkFlow GetWorkFlow(int workFlowId);

        int CreateWorkFlow(WorkFlow workFlow);
        IEnumerable<Examination> GetExaminations(int patientId);

        Examination GetExamination(int patientId, int examinationId);
        Examination GetExamination(int examinationId);

        void AddExaminationToPatient(int patientId, Examination exam, ExaminationType examType, IExaminationType examData);

        bool Save();

        void DeleteExam(Examination exam);

        void AddPatient(Patient patient);

        bool PatientExistsByExtId(int extId);

        Patient GetPatientByExtID(int extId);

        IExaminationType GetExaminationDetail(int patientId, int examinationId);

        void UpdateExaminationStatus(int examId, bool arc);

    }
}
