using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHapi.Base.Model;
using NHapi.Base.Util;
using NHapiTools.Base.Net;
using NSService.Common;
using NSService.Entities;
using NSService.Protocol;
using NSService.Services;

namespace NSService.Controllers
{
    [Route("api/HL7Com")]
    public class HL7ComController : Controller
    {
        // Add-migration, update database
        // Protocol fields.
        string hostname = "localhost";
        int port = 2020;
        private IPatientInfoRepository _patientInfoRepository;
        HL7CommunicationService hL7CommunicationService;
        private ILogger<ExaminationController> _logger;

        public HL7ComController(ILogger<ExaminationController> logger, IPatientInfoRepository patientInfoRepository)
        {
            hL7CommunicationService = new HL7CommunicationService(hostname, port);
            _patientInfoRepository = patientInfoRepository;
            _logger = logger;
        }

        [HttpGet()]
        [HttpPost()]
        public IActionResult HL7messageFromServer()
        {
            String hl7MessageRaw;

            using (Stream Body = HttpContext.Request.Body)
            {
                hl7MessageRaw = new StreamReader(HttpContext.Request.Body).ReadToEnd();
            }

            HL7RequestInfo info = hL7CommunicationService.ParseHL7RawMessage(hl7MessageRaw, "Http");
            
            HadleHL7Message(info, hl7MessageRaw);
            IMessage response = HL7Acknowlege.MakeACK(info.Message, "AA");
            //Terser terser = new Terser(response);
            return Accepted(response);
        }

        /// <summary>
        ///  Sort and save to DB HL7 incoming Messages.
        /// </summary>
        /// <param name="request"></param>
        void HadleHL7Message(HL7RequestInfo request, string createdFromHl7Message)
        {
            try
            {
                // Handle ADT_A01 - incoming patient registration. Also save the raw HL7 Messages. To log and store it.
                if (request.Message.ToString().Contains("ADT_A01"))
                {
                    var pateitnToAdd = new Entities.Patient()
                    {
                        Name = ((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.PatientName.GivenName.Value + " " + ((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.PatientName.FamilyName.Value,
                        Gender = ((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.Sex.Value,
                        ExternalId = Convert.ToInt32(((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.PatientAccountNumber.ID.Value),
                        Age = ((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.DateOfBirth.TimeOfAnEvent.Year,
                        BirthDate = new DateTime(
                         ((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.DateOfBirth.TimeOfAnEvent.Year,
                         ((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.DateOfBirth.TimeOfAnEvent.Month,
                         ((NHapi.Model.V23.Message.ADT_A01)request.Message).PID.DateOfBirth.TimeOfAnEvent.Day),
                         OriginalHL7Message = createdFromHl7Message,

                    };

                    _patientInfoRepository.AddPatient(pateitnToAdd);
                    _patientInfoRepository.Save();
                }
                // Handle  ORU_R01 - incoming Examination data.
                if (request.Message.ToString().Contains("ORU_R01"))
                {
                    int externalID = Convert.ToInt32(((NHapi.Model.V23.Message.ORU_R01)request.Message).GetRESPONSE().PATIENT.PID.PatientAccountNumber.ID.Value);
                    Patient patient = new Patient();
                    Examination examToAdd = new Examination();
                    BloodPressureData newExamData = new BloodPressureData();
                    SpOData newExaamSPo = new SpOData();
                    BodyTemperatureData newExamBTD = new BodyTemperatureData();
                    bool BloodPressureFalg = false;
                    if (_patientInfoRepository.PatientExistsByExtId(externalID))
                    {
                        patient = _patientInfoRepository.GetPatientByExtID(externalID);
                    }
                    else
                    {
                        // Todo Save Patient.
                    }

                    int obsCount = ((NHapi.Model.V23.Message.ORU_R01)request.Message).GetRESPONSE().ORDER_OBSERVATIONRepetitionsUsed;
                    for (int i = 0; i < obsCount; i++)
                    {
                        var orderObservation = ((NHapi.Model.V23.Message.ORU_R01)request.Message).GetRESPONSE().GetORDER_OBSERVATION(i);
                        int obxCount = ((NHapi.Model.V23.Message.ORU_R01)request.Message).GetRESPONSE().GetORDER_OBSERVATION(i).OBSERVATIONRepetitionsUsed;
                        for (int j = 0; j < obxCount; j++)
                        {
                            NHapi.Model.V23.Segment.OBX obx = orderObservation.GetOBSERVATION(j).OBX;
                            var obxVaries = orderObservation.GetOBSERVATION(j).OBX.GetObservationValue();

                            if (obx.ObservationIdentifier.Text.Value == "Body temperature")
                            {
                                Examination examToAddBDT= new Examination();
                                examToAddBDT.Description = String.Empty;
                                examToAddBDT.PatientId = patient.Id;
                                examToAddBDT.Value = DateTime.Now.ToString();
                                examToAddBDT.ExaminationType = "Body temperature";
                                newExamBTD.TemperatureValue= Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                                _patientInfoRepository.AddExaminationToPatient(patient.Id, examToAddBDT, ExaminationType.BodyTemperature, newExamBTD);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "SpO2")
                            {
                                Examination examToAddSPO = new Examination();
                                examToAddSPO.PatientId = patient.Id;
                                examToAddSPO.Description = String.Empty;
                                examToAddSPO.Value = DateTime.Now.ToString();
                                examToAddSPO.ExaminationType = "SpO2";
                                newExaamSPo.SPOValue = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                                _patientInfoRepository.AddExaminationToPatient(patient.Id, examToAddSPO, ExaminationType.BloodSpO2, newExaamSPo);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Mean blood pressure")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Description = String.Empty;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.MeanBloodPressure = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Pulse rate")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Description = String.Empty;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.PulseRate = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Diastolic blood pressure")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Description = String.Empty;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.DiastolicValue = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Systolic blood pressure")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Description = String.Empty;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.SystolicValue = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                        }
                        if (BloodPressureFalg)
                        {
                            _patientInfoRepository.AddExaminationToPatient(patient.Id, examToAdd, ExaminationType.BloodPressure, newExamData);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("HadleHL7Message error: " + ex.Message);
            }
           // request.Message
        }
    }
}