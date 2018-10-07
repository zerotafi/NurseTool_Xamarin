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
        // Patient part.
        bool PatientExists(int patientId);
        void AddPatient(Patient patient);
        bool PatientExistsByExtId(int extId);
        Patient GetPatientByExtID(int extId);
        IEnumerable<Patient> GetPatients();
        Patient GetPatient(int patientId, bool includeExaminations = false);
       
        // General and auth part.
        bool Save();
        bool Auth(string username, string password);
        User GetUserById(int userId);

        // Exam part.
        IExaminationType GetExaminationDetail(int patientId, int examinationId);
        void UpdateExaminationStatus(int examId, bool arc);
        void DeleteExam(Examination exam);
        void AddExaminationToPatient(int patientId, Examination exam, ExaminationType examType, IExaminationType examData, WorkFlow workFlow);
        IEnumerable<Examination> GetExaminations(int patientId);
        Examination GetExamination(int patientId, int examinationId);
        Examination GetExamination(int examinationId);

        // WorkFlow Part.
        WorkFlow GetWorkFlow(int workFlowId);
        List<WorkFlow> GetWorkFlowsForPatients(int patientId);
        int? CreateWorkFlow(WorkFlow workFlow);
        List<WorkFlowStep> GetworkFlowSteps(int workFlowId);
        WorkFlowStep GetworkFlowStep(int workFlowId, int workFlowStepId);
        int AddWorkFowStepToWorkFlow(int workFlowId, WorkFlowStep workFlowStep);

    }
}
