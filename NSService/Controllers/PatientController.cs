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
            _patientInfoRepository = patientInfoRepository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetAllPatient()
        {
            var patients = _patientInfoRepository.GetPatients();
            var result = Mapper.Map<IEnumerable<PatientWithoutExaminationDTO>>(patients);

            return Ok(result);
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