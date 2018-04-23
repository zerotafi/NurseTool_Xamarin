using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSService.Common;
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
            return _context.Patients.Include(x => x.Examinations).FirstOrDefault(x => x.Id == patientId).Examinations.FirstOrDefault(x => x.Id == examinationId);
        }

        public IExaminationType GetExaminationDetail(int patientId, int examinationId)
        {
            var exam = _context.Patients.Include(x => x.Examinations).FirstOrDefault(x => x.Id == patientId).Examinations.FirstOrDefault(x => x.Id == examinationId);
            if (exam.ExaminationType == "SpO2")
            {
               return _context.SpOData.FirstOrDefault(x => x.ExaminationId == exam.Id);
            }
            if (exam.ExaminationType == "BloodPressure")
            {
                return _context.BloodPressureData.FirstOrDefault(x => x.ExaminationId == exam.Id);
            }
            else
            {
                return _context.BodyTemperatureData.FirstOrDefault(x => x.ExaminationId == exam.Id);
            }

        }

        public IEnumerable<Examination> GetExaminations(int patientId)
        {
            IEnumerable<Examination> result = _context.Patients.Include(x => x.Examinations).FirstOrDefault(x => x.Id == patientId).Examinations.ToList();
            return result;
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

        public Patient GetPatientByExtID(int extId)
        {
            return _context.Patients.FirstOrDefault(x => x.ExternalId == extId);
        }

        public bool PatientExistsByExtId(int extId)
        {
            return _context.Patients.Any(x => x.ExternalId == extId);
        }

        public void AddExaminationToPatient(int patientId, Examination exam, ExaminationType examType, IExaminationType examData)
        {
            var patient = GetPatient(patientId, true);
            patient.Examinations.Add(exam);
            _context.SaveChanges();
            if (examType == ExaminationType.BloodPressure)
            {
                BloodPressureData ExanData = examData as BloodPressureData;
                ExanData.ExaminationId = exam.Id;
                _context.BloodPressureData.Add(examData as BloodPressureData);
                _context.SaveChanges();
            }
            if (examType == ExaminationType.BloodSpO2)
            {
                SpOData ExanData = examData as SpOData;
                ExanData.ExaminationId = exam.Id;
                _context.SpOData.Add(examData as SpOData);
                _context.SaveChanges();
            }
            if (examType == ExaminationType.BodyTemperature)
            {
                BodyTemperatureData ExanData = examData as BodyTemperatureData;
                ExanData.ExaminationId = exam.Id;
                _context.BodyTemperatureData.Add(examData as BodyTemperatureData);
                _context.SaveChanges();
            }
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
