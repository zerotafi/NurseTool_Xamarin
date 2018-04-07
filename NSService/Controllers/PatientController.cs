using Microsoft.AspNetCore.Mvc;
using NSService.Models;
using NSService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Controllers
{
    [Route("api/patients")]
    public class PatientController : Controller
    {
        private IPatientInfoRepository _patientInfoRepository;

        public PatientController(IPatientInfoRepository patientInfoRepository)
        {
            _patientInfoRepository = patientInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetAllPatient()
        {
            var patients = _patientInfoRepository.GetPatients();
            var result = new List<PatientWithoutExaminationDTO>();

            foreach (var patient in patients)
            {
                result.Add( new PatientWithoutExaminationDTO()
                {
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Id = patient.Id,
                    Name = patient.Name
                });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetPatient(int id, bool includeExaminations = false)
        {
            var patient = _patientInfoRepository.GetPatient(id, includeExaminations);

            if (patient == null) { return NotFound(); }

            if (includeExaminations)
            {
                var patientResult = new PatientDTO()
                {
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Id = patient.Id,
                    Name = patient.Name
                };

                foreach (var exam in patient.Examinations)
                {
                    patientResult.Examinations.Add(
                        new ExaminationsDTO()
                        {
                            Description = exam.Description,
                            Id = exam.Id,
                            PatientId = exam.PatientId,
                            ExaminationType = exam.ExaminationType,
                            Value = exam.Value
                        });
                }
                return Ok(patientResult);
            }
            else
            {
                var patientResult = new PatientDTO()
                {
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Id = patient.Id,
                    Name = patient.Name
                };

                return Ok(patientResult);
            }
        }

    }
}
