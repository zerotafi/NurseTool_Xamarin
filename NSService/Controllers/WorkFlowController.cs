﻿using System;
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

        [HttpGet("{workflowId}/workFlow")]
        public IActionResult GetWorkFlow(int workflowId)
        {
            var workflow = _patientInfoRepository.GetWorkFlow(workflowId);

            if (workflow == null) { return NotFound(); }

            var workflowResult = Mapper.Map<PatientDTO>(workflow);

            return Ok(workflowResult);
        }

        [HttpGet("patient/{patientId}")]
        public IActionResult GetWorkFlowForPatient(int patientId)
        {
            var workflow = _patientInfoRepository.GetWorkFlowsForPatients(patientId);

            if (workflow == null) { return NotFound(); }

            var workflowResult = Mapper.Map<IEnumerable<WorkFlowDTO>>(workflow);

            return Ok(workflowResult);
        }

        [HttpGet("patient/{patientId}/userInfo/{userName}")]
        public IActionResult CreateWorkFlow(int patientId, string userName)
        {
            WorkFlow workFlow = new WorkFlow();
            var patientFound =_patientInfoRepository.GetPatient(patientId, false);
            if (patientFound == null) { return NotFound(); }
            workFlow.Patient = patientFound;
            var userFound = _patientInfoRepository.GetUserByName(userName);
            if (userFound == null) { return NotFound(); }
            workFlow.Username = userFound.Username;
            workFlow.WorkFlowName = "default - not set";
            int? workflowID = _patientInfoRepository.CreateWorkFlow(workFlow);
            if (workflowID == null) { return NotFound(); }
            return Ok(workflowID);
        }

        [HttpGet("workFlowId/{workFlowID}/wfStepName/{wfStepName}")]
        public IActionResult AddWorkFlowStepToWorkFlow(int workFlowID, string wfStepName)
        {
            WorkFlowStep wfStep = new WorkFlowStep();
            wfStep.WorkFlowStepName = wfStepName;
            int wfStepID = _patientInfoRepository.AddWorkFowStepToWorkFlow(workFlowID, wfStep);
            return Ok(wfStepID);
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