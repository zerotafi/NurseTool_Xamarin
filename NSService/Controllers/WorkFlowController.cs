using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSService.Entities;
using NSService.Models;
using NSService.Services;

namespace NSService.Controllers
{
    [Route("api/workflow")]
    public class WorkFlowController : Controller
    {
        private IPatientInfoRepository _patientInfoRepository;
        private ILogger<ExaminationController> _logger;

        public WorkFlowController(ILogger<ExaminationController> logger, IPatientInfoRepository patientInfoRepositor)
        {
            _patientInfoRepository = patientInfoRepositor;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkFlow(int workflowId)
        {
            var workflow = _patientInfoRepository.GetWorkFlow(workflowId);

            if (workflow == null) { return NotFound(); }

            var workflowResult = Mapper.Map<PatientDTO>(workflow);

            return Ok(workflowResult);
        }

        [HttpGet("{id}")]
        public IActionResult CreateWorkFlow(int patient, int userId)
        {
            WorkFlow workFlow = new WorkFlow();
            var workflow = _patientInfoRepository.CreateWorkFlow(workFlow);

            if (workflow == null) { return NotFound(); }

            var workflowResult = Mapper.Map<PatientDTO>(workflow);

            return Ok(workflowResult);
        }

        [HttpPost]
        public IActionResult SaveWorkFlow()
        {
            return View();
        }

        [HttpGet("{workflowId}")]
        public IActionResult AddNewItemForWorkFlow(int workflowId)
        {
            return View();
        }
    }
}