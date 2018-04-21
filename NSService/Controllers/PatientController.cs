using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<ExaminationController> _logger;

        public PatientController(ILogger<ExaminationController> logger, IPatientInfoRepository patientInfoRepository)
        {
            _logger = logger;
            try
            {
                _logger.LogInformation("Create patient controller.");
                  _patientInfoRepository = patientInfoRepository;
                _logger.LogInformation("Patient controller created."+ patientInfoRepository.GetPatient(1));
            }
            catch (Exception ex)
            {
                _logger.LogError("PatientController init error: " + ex.Message.ToString());
            }
           
        }

        [HttpGet()]
        public IActionResult GetAllPatient()
        {
            try
            {
                _logger.LogInformation("GetAllPatient Called");
                var patients = _patientInfoRepository.GetPatients();
                var result = Mapper.Map<IEnumerable<PatientWithoutExaminationDTO>>(patients);
                if (result == null) { return StatusCode(500, "no result"); }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetPatient(int id, bool includeExaminations = false)
        {
            var patient = _patientInfoRepository.GetPatient(id, includeExaminations);

            if (patient == null) { return NotFound(); }

            if (includeExaminations)
            {
                var patientResult = Mapper.Map<PatientDTO>(patient);

                return Ok(patientResult);
            }
            else
            {
                var patientResult = Mapper.Map<PatientWithoutExaminationDTO>(patient);

                return Ok(patientResult);
            }
        }

        [HttpPost("")]
        public IActionResult CreatePatient([FromBody] PatientCreationDTO patientDTO)
        {
            if (patientDTO == null)
            {
                BadRequest();
            }

            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var pateitnToAdd = Mapper.Map<Entities.Patient>(patientDTO);

            _patientInfoRepository.AddPatient(pateitnToAdd);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "Internal Server Error");
            }

            var patientToAddResult = Mapper.Map<PatientCreationDTO>(pateitnToAdd);

            return CreatedAtRoute("GetExamination", new { patientID = pateitnToAdd.Id, patientToAddResult });
        }
    }
}