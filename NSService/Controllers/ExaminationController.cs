using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSService.Entities;
using NSService.Models;
using NSService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Controllers
{
    [Route("api/patients")]
    public class ExaminationController : Controller
    {
        private ILogger<ExaminationController> _logger;

        private IPatientInfoRepository _patientInfoRepository;

        public ExaminationController(ILogger<ExaminationController> logger, IPatientInfoRepository patientInfoRepository)
        {
            _logger = logger;
            _patientInfoRepository = patientInfoRepository;
        }

        [HttpGet("{patientId}/examination")]
        public IActionResult GetExaminations(int patientId)
        {
            try
            {
                if (!_patientInfoRepository.PatientExists(patientId))
                {
                    _logger.LogInformation("Patient not exist PatientID: " + patientId);
                    return NotFound();
                }

                var exams = _patientInfoRepository.GetExaminations(patientId);

                if (exams == null)
                {
                    _logger.LogInformation("Exams not found PatientID: " + patientId);
                    return NotFound();
                }

                var examsDTOList = Mapper.Map<List<ExaminationsDTO>>(exams); 

                return Ok(examsDTOList);
            }
            catch(Exception ex)

            {
                _logger.LogCritical("GetExaminations() Error: " + ex.Message.ToString());
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("{patientId}/examination/{exmiantionId}", Name = "GetExamination")]
        public IActionResult GetExaminationsByID(int patientId, int exmiantionId)
        {
            if(!_patientInfoRepository.PatientExists(patientId))
                {
                _logger.LogInformation("Patient not exist PatientID: " + patientId);
                return NotFound();
            }

            var examination = _patientInfoRepository.GetExamination(patientId, exmiantionId);

            var examinationDetail =_patientInfoRepository.GetExaminationDetail(patientId, exmiantionId);

            ExaminationDetailDTO examinationResult = new ExaminationDetailDTO();
            examinationResult.Description = examination.ExaminationType;

            if (examination == null)
            {
                return NotFound();
            }

            if (examinationDetail is SpOData)
            {
                examinationResult.SPOValue = (examinationDetail as SpOData).SPOValue;
                return Ok(examinationResult);
            }
            if (examinationDetail is BloodPressureData)
            {
                examinationResult.MeanBloodPressure = (examinationDetail as BloodPressureData).MeanBloodPressure;
                examinationResult.PulseRate = (examinationDetail as BloodPressureData).PulseRate;
                examinationResult.SystolicValue = (examinationDetail as BloodPressureData).SystolicValue;
                examinationResult.DiastolicValue = (examinationDetail as BloodPressureData).DiastolicValue;
                return Ok(examinationResult);
            }
            if (examinationDetail is BodyTemperatureData)
            {
                examinationResult.TemperatureValue = (examinationDetail as BodyTemperatureData).TemperatureValue;
                return Ok(examinationResult);
            }

            return NotFound();

        }

        [HttpPost("{patientId}/examination/")]
        public IActionResult CreateExamination(int patientId, [FromBody] ExaminationCreationDTO examinationDTO)
        {
            if (examinationDTO == null)
            {
                BadRequest();
            }

            if(!ModelState.IsValid)
            { 
                BadRequest();
            }

            if (!_patientInfoRepository.PatientExists(patientId))
            { 
                return NotFound();
            }

            var ExaminationNew = Mapper.Map<Entities.Examination>(examinationDTO);

            _patientInfoRepository.AddExaminationToPatient(patientId, ExaminationNew, Common.ExaminationType.BloodPressure, null);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "Internal Server Error");
            }

            var ExaminationResult = Mapper.Map<ExaminationCreationDTO>(ExaminationNew);

            return CreatedAtRoute("GetExamination", new { patientID = patientId , ExaminationResult });
        }

        [HttpPut("{patientId}/examination/{exmiantionId}")]
        public IActionResult UpdateExamination(int patientId, int exmiantionId, [FromBody] ExamiantionUpdateDTO examinationDTO)
        {
            if (examinationDTO == null)
            {
                BadRequest();
            }

            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            if (_patientInfoRepository.PatientExists(patientId))
            {
                return NotFound();
            }

            var examination = _patientInfoRepository.GetExamination(patientId, exmiantionId); 

            if (examination == null)
            {
                return NotFound();
            }

            Mapper.Map(examinationDTO, examination);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "Internal Server Error");
            }

            return NoContent();
        }

        [HttpPatch("{patientId}/examination/{exmiantionId}")]
        public IActionResult PartiallyUpdateExamination(int patientId, int exmiantionId, [FromBody] JsonPatchDocument<ExamiantionUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                BadRequest();
            }
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            if (_patientInfoRepository.PatientExists(patientId))
            {
                return NotFound();
            }

            var examination = _patientInfoRepository.GetExamination(patientId, exmiantionId);

            if (examination == null)
            {
                return NotFound();
            }

            var examinationToPatch = Mapper.Map<ExamiantionUpdateDTO>(examination); 

            patchDoc.ApplyTo(examinationToPatch);

            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            Mapper.Map(examinationToPatch, examination);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "Internal Server Error");
            }
            return NoContent();
        }

        [HttpDelete("{patientId}/examination/{exmiantionId}")]
        public IActionResult DeleteExamiantion(int patientId, int exmiantionId)
        {
            if (_patientInfoRepository.PatientExists(patientId))
            {
                return NotFound();
            }

            var examination = _patientInfoRepository.GetExamination(patientId, exmiantionId);

            if (examination == null)
            {
                return NotFound();
            }
            _patientInfoRepository.DeleteExam(examination);
            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "Internal Server Error");
            }

            return NoContent();
        }

    }
}
