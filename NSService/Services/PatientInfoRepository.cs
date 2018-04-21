using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSService.Entities;

namespace NSService.Services
{
    public class PatientInfoRepository : IPatientInfoRepository
    {
        NLog.Logger _logger;
        public PatientInfoContext _context;

        public PatientInfoRepository(PatientInfoContext context)
        {
            _context = context;
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _logger.Log(NLog.LogLevel.Info, "PatientInfoRepository  created.");
        }

        public bool PatientExists(int patientId)
        {
            return _context.Patients.Any(x => x.Id == patientId);
        }

        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        public Examination GetExamination(int patientId, int examinationId)
        {
            return _context.Patients.FirstOrDefault(x => x.Id == patientId).Examinations.FirstOrDefault(x => x.Id == examinationId);
        }

        public IEnumerable<Examination> GetExaminations(int patientId)
        {
            return _context.Patients.FirstOrDefault(x => x.Id == patientId).Examinations.ToList();
        }

        public Patient GetPatient(int patientId, bool includeExaminations)
        {
            if (includeExaminations)
            {
                return _context.Patients.Include(x => x.Examinations).Where(x => x.Id == patientId).FirstOrDefault();
            }
            else
            {
                return _context.Patients.Where(x => x.Id == patientId).FirstOrDefault();
            }
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients.OrderBy(x => x.Name).ToList();
        }


        public void AddExaminationToPatient(int patientId, Examination exam)
        {
            var patient = GetPatient(patientId, true);
            patient.Examinations.Add(exam);
        }

        public void DeleteExam(Examination exam)
        {
            _context.Examinations.Remove(exam);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
