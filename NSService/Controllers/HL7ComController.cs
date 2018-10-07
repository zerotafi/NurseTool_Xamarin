using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HL7Messaging;
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

        [HttpGet("send/{exmiantionId}")]
        public IActionResult HL7messageSendToServer(int exmiantionId)
        {
            try
            {

                var examination = _patientInfoRepository.GetExamination(exmiantionId);

                if (examination == null)
                {
                    return NotFound();
                }

                var patient = _patientInfoRepository.GetPatient(examination.PatientId);

                if (patient == null)
                {
                    return NotFound();
                }

                var examDetail = _patientInfoRepository.GetExaminationDetail(patient.Id, examination.Id);

                if (examDetail == null)
                {
                    return NotFound();
                }

                Message msg = CreateHL7Message(examination, "2.3", patient, examDetail);

                string hl7msg = msg.Serialize();
                HL7RequestInfo info = hL7CommunicationService.ParseHL7RawMessage(hl7msg, "Http");
                SimpleMLLPClient client = new SimpleMLLPClient("localhost", 2021);
                //MLLPSession session = new MLLPSession();
                var result = client.SendHL7Message(info.Message);
                _patientInfoRepository.UpdateExaminationStatus(exmiantionId, true);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
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
            NHapi.Model.V23.Message.ADT_A01 message = ((NHapi.Model.V23.Message.ADT_A01)info.Message);

            HadleHL7Message(info, hl7MessageRaw);
            IMessage response = HL7Acknowlege.MakeACK(info.Message, "AA");
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
                        Archived = true,
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
                                examToAddBDT.Archived = true;
                                examToAddBDT.PatientId = patient.Id;
                                examToAddBDT.Value = DateTime.Now.ToString();
                                examToAddBDT.ExaminationType = "Body temperature";
                                newExamBTD.TemperatureValue= Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                                _patientInfoRepository.AddExaminationToPatient(patient.Id, examToAddBDT, ExaminationType.BodyTemperature, newExamBTD, null);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "SpO2")
                            {
                                Examination examToAddSPO = new Examination();
                                examToAddSPO.PatientId = patient.Id;
                                examToAddSPO.Description = String.Empty;
                                examToAddSPO.Archived = true;
                                examToAddSPO.Value = DateTime.Now.ToString();
                                examToAddSPO.ExaminationType = "SpO2";
                                newExaamSPo.SPOValue = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                                _patientInfoRepository.AddExaminationToPatient(patient.Id, examToAddSPO, ExaminationType.BloodSpO2, newExaamSPo, null);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Mean blood pressure")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Description = String.Empty;
                                examToAdd.Archived = true;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.MeanBloodPressure = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Pulse rate")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Archived = true;
                                examToAdd.Description = String.Empty;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.PulseRate = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Diastolic blood pressure")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Archived = true;
                                examToAdd.Description = String.Empty;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.DiastolicValue = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                            if (obx.ObservationIdentifier.Text.Value == "Systolic blood pressure")
                            {
                                BloodPressureFalg = true;
                                examToAdd.PatientId = patient.Id;
                                examToAdd.Archived = true;
                                examToAdd.Description = String.Empty;
                                examToAdd.Value = DateTime.Now.ToString();
                                examToAdd.ExaminationType = "BloodPressure";
                                newExamData.SystolicValue = Convert.ToInt32(((NHapi.Base.Model.AbstractPrimitive)obx.GetObservationValue(0).Data).Value);
                            }
                        }
                        if (BloodPressureFalg)
                        {
                            _patientInfoRepository.AddExaminationToPatient(patient.Id, examToAdd, ExaminationType.BloodPressure, newExamData, null);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("HadleHL7Message error: " + ex.Message);
            }
        }

        Message CreateHL7Message(Examination exam, string version, Patient patient, IExaminationType examType)
        {
            Message msg = new Message();
            var msh = new Segment();
            // MSH|^~\&|LIS|M|||20090518161040||ORU^R01|91380000032|P|2.3|
            msh.Field(0, @"MSH");
            msh.Field(2, @"^~\&");
            msh.Field(3, "NurseCube");
            msh.Field(4, "M");
            msh.Field(5, "");
            msh.Field(6, "");
            string examDate = DateTime.Parse(exam.Value).ToString("yyyyMMddHHmmss");
            msh.Field(7, examDate);
            msh.Field(8, "");
            msh.Field(9, "ORU^R01");
            msh.Field(10, exam.Id.ToString());
            msh.Field(11, "P");
            msh.Field(12, version);

            msg.Add(msh);

            var pid = new Segment();
            // PID|||15161516^^^^M||TEST^EMR SAMPLE^||19651015|M||||||||||1719|
            pid.Field(0, "PID");
            pid.Field(1, "");
            pid.Field(2, patient.ExternalId.ToString());
            pid.Field(3, patient.Id.ToString());
            pid.Field(4, "");
            pid.Field(5, patient.Name);
            pid.Field(6, "PID");
            string birthDate = DateTime.Parse(patient.BirthDate.ToString()).ToString("yyyyMMdd");
            pid.Field(7, birthDate);
            pid.Field(8, patient.Gender.ToString());
            pid.Field(9, "");
            pid.Field(10, "");
            pid.Field(11, "");
            pid.Field(12, "");
            pid.Field(13, "");
            pid.Field(14, "");
            pid.Field(15, "");
            pid.Field(16, "");
            pid.Field(17, "");
            pid.Field(17, patient.ExternalId.ToString());
            msg.Add(pid);

            // ORC | RE |||||||||||||||^|

            var obc = new Segment();
            obc.Field(0, "ORC");
            obc.Field(1, "RE");
            for (int i = 0; i < 14; i++)
            {
                obc.Field(i + 2, "");
            }
            obc.Field(1, "^");
            msg.Add(obc);

            var obr = new Segment();
            //OBR|||E2905964|^^^ADIF^CBC|||200905041213|||||||200905041223|^|14516^TEST^PHYSICIAN||||M3017||||H|F|CBC^ADIF|^^^^^R|^^~
            obr.Field(0, "OBR");
            obr.Field(1, "");
            obr.Field(2, "");
            obr.Field(3, "E2905977");
            obr.Field(4, "^^^ADIF^CBC");
            obr.Field(5, "");
            obr.Field(6, "");
            obr.Field(7, examDate);
            obr.Field(8, "");
            obr.Field(9, "");
            obr.Field(10, "");
            obr.Field(11, "");
            obr.Field(12, "");
            obr.Field(13, "");

            obr.Field(14, examDate);
            obr.Field(15, "^");
            // ToDO Add nurse id
            obr.Field(16, "14516^TEST^Nurse");
            obr.Field(17, "");
            obr.Field(18, "");
            msg.Add(obr);

            if (exam.ExaminationType == "SpO2")
            {
                var obx = new Segment();
                // OBX | 6 | NM | 431314004 ^ SpO2 ^ SNOMED - CT || 90 |%| 94 - 100 | L ||| F ||| 20100511220525
                obx.Field(0, "OBX");
                obx.Field(1, "6");
                obx.Field(2, "NM");
                obx.Field(3, "SpO2");
                obx.Field(4, "");
                obx.Field(5, (examType as SpOData).SPOValue.ToString());
                obx.Field(6, "%");
                obx.Field(7, "94 - 100");
                obx.Field(8, "L");
                obx.Field(9, "");
                obx.Field(10, "");
                obx.Field(11, "F");
                obx.Field(12, "");
                obx.Field(13, "");
                obx.Field(14, examDate);

                msg.Add(obx);
            }

            if (exam.ExaminationType == "Body temperature")
            {
                var obx = new Segment();
                //OBX|2|NM|386725007 ^Body temperature ^SNOMED-CT||37|C |37|N|||F|||20100511220525
                obx.Field(0, "OBX");
                obx.Field(1, "2");
                obx.Field(2, "NM");
                obx.Field(3, "Body temperature");
                obx.Field(4, "");
                obx.Field(5, (examType as BodyTemperatureData).TemperatureValue.ToString());
                obx.Field(6, "");
                obx.Field(7, "37");
                obx.Field(8, "C");
                obx.Field(9, "37");
                obx.Field(10, "");
                obx.Field(11, "");
                obx.Field(12, "");
                obx.Field(13, "");
                obx.Field(14, examDate);

                msg.Add(obx);
            }

            if (exam.ExaminationType == "BloodPressure")
            {
                var obxmBP = new Segment();
                //OBX | 1 | NM | 6797001 ^ Mean blood pressure ^ SNOMED - CT || 94 | mm[Hg] | 92 - 96 | N ||| F ||| 20100511220525
        
                obxmBP.Field(0, "OBX");
                obxmBP.Field(1, "1");
                obxmBP.Field(2, "NM");
                obxmBP.Field(3, "Mean blood pressure");
                obxmBP.Field(4, "");
                obxmBP.Field(5, (examType as BloodPressureData).MeanBloodPressure.ToString());
                obxmBP.Field(6, "");
                obxmBP.Field(7, "94");
                obxmBP.Field(8, "mm[Hg]");
                obxmBP.Field(9, "92 - 96");
                obxmBP.Field(10, "n");
                obxmBP.Field(11, "");
                obxmBP.Field(12, "");
                obxmBP.Field(13, "F");
                obxmBP.Field(14, examDate);
                msg.Add(obxmBP);

                var obxmBT = new Segment();
                //OBX | 3 | NM | 271649006 ^ Systolic blood pressure ^ SNOMED - CT || 100 | mm[Hg] | 90 - 120 | N ||| F ||| 20100511220725
                obxmBT.Field(0, "OBX");
                obxmBT.Field(1, "2");
                obxmBT.Field(2, "NM");
                obxmBT.Field(3, "Systolic blood pressur");
                obxmBT.Field(4, "");
                obxmBT.Field(5, (examType as BloodPressureData).SystolicValue.ToString());
                obxmBT.Field(6, "");
                obxmBT.Field(7, "100");
                obxmBT.Field(8, "mm[Hg]");
                obxmBT.Field(9, "90-120");
                obxmBT.Field(10, "n");
                obxmBT.Field(11, "");
                obxmBT.Field(12, "");
                obxmBT.Field(13, "F");
                obxmBT.Field(14, examDate);
                msg.Add(obxmBT);

                //OBX | 4 | NM | 271650006 ^ Diastolic blood pressure ^ SNOMED - CT || 68 | mm[Hg] | 60 - 80 | N ||| F ||| 20100511220725
                var obxmDT = new Segment();
                obxmBT.Field(0, "OBX");
                obxmBT.Field(1, "3");
                obxmDT.Field(2, "NM");
                obxmDT.Field(3, "Diastolic blood pressure");
                obxmDT.Field(4, "");
                obxmDT.Field(5, (examType as BloodPressureData).DiastolicValue.ToString());
                obxmDT.Field(6, "");
                obxmDT.Field(7, "68");
                obxmDT.Field(8, "mm[Hg]");
                obxmDT.Field(9, "60-80");
                obxmDT.Field(10, "n");
                obxmDT.Field(11, "");
                obxmDT.Field(12, "");
                obxmDT.Field(13, "F");
                obxmDT.Field(14, examDate);
                msg.Add(obxmDT);

                //OBX|5|NM|78564009 ^Pulse rate ^SNOMED-CT||80|bpm |60-100|N|||F|||20100511220525
                var obxmPR = new Segment();
                obxmPR.Field(0, "OBX");
                obxmPR.Field(1, "4");
                obxmPR.Field(2, "NM");
                obxmPR.Field(3, "Pulse rate");
                obxmPR.Field(4, "");
                obxmPR.Field(5, (examType as BloodPressureData).PulseRate.ToString());
                obxmPR.Field(6, "");
                obxmPR.Field(7, "80");
                obxmPR.Field(8, "mm[Hg]");
                obxmPR.Field(9, "60-100");
                obxmPR.Field(10, "n");
                obxmPR.Field(11, "");
                obxmPR.Field(12, "");
                obxmPR.Field(13, "F");
                obxmPR.Field(14, examDate);
                msg.Add(obxmPR);
            }

            return msg;
        }



        
    }
}