using Microsoft.AspNetCore.Mvc;
using NSService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Controllers
{
    [Route("api/patients")]
    public class ExaminationController : Controller
    {
        [HttpGet("{patientId}/examination")]
        public IActionResult GetExaminations(int patientId)
        {
            var patient = PatientDataStore.Current.Patients.FirstOrDefault(x => x.Id == patientId);

            if(patient == null)
            {
                return NotFound();
            }

            return Ok(patient.Examinations);
        }

        [HttpGet("{patientId}/examination/{exmiantionId}", Name = "GetExamination")]
        public IActionResult GetExaminationsByID(int patientId, int exmiantionId)
        {
            var patient = PatientDataStore.Current.Patients.FirstOrDefault(x => x.Id == patientId);

            if (patient == null)
            {
                return NotFound();
            }

            var examination = patient.Examinations.FirstOrDefault(x => x.Id == exmiantionId);

            if(examination == null)
            {
                return NotFound();
            }

            return Ok(examination);

        }

        [HttpPost("{patientId}/examination/")]
        public IActionResult CreateExamination(int patientId, [FromBody] ExaminationCreationDTO examinationDTO)
        {
            if (examinationDTO == null)
            {
                BadRequest();
            }
            var patient = PatientDataStore.Current.Patients.FirstOrDefault(x => x.Id == patientId);

            if (patient == null)
            {
                return NotFound();
            }

            var maxID = PatientDataStore.Current.Patients.SelectMany(x => x.Examinations).Max(p => p.Id);
            var ExaminationNew = new ExaminationsDTO()
            {
                 Id = maxID+1,
                 Description = "BloodPresure",
                 PatientId = patientId,
                 Value = "Test new Examination"
            };
            patient.Examinations.Add(ExaminationNew);

            return CreatedAtRoute("GetExamination", new { patientID = patientId , id = maxID , ExaminationNew });
        }


    }
}
