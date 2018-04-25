using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class ExaminationViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;

        public ExaminationDetail examinationDetail { get; set; }

        public Patient deatilPatient { get; set; }

        public String Description
        {
            get { return examinationDetail.description; }
            set { examinationDetail.description = value; }
        }

        public bool TemperatureVisible
        {
            get { return examinationDetail.temperatureValue != null; }
        }

        public int? Temperature
        {
            get { return examinationDetail.temperatureValue; }
            set { examinationDetail.temperatureValue = value; }
        }

        public bool SpoValueVisible
        {
            get { return examinationDetail.spoValue != null; }
        }

        public int? SpoValue
        {
            get { return examinationDetail.spoValue; }
            set { examinationDetail.spoValue = value; }
        }

        public bool SystolicVisible
        {
            get { return examinationDetail.systolicValue != null; }
        }

        public int? SystolicValue
        {
            get { return examinationDetail.systolicValue; }
            set { examinationDetail.systolicValue = value; }
        }

        public bool DiastolicVisible
        {
            get { return examinationDetail.diastolicValue != null; }
        }

        public int? DiastolicValue
        {
            get { return examinationDetail.diastolicValue; }
            set { examinationDetail.diastolicValue = value; }
        }

        public bool MeanBloodPressureVisible
        {
            get { return examinationDetail.meanBloodPressure != null; }
        }


        public int? MeanBloodPressure
        {
            get { return examinationDetail.meanBloodPressure; }
            set { examinationDetail.meanBloodPressure = value; }
        }

        public bool PulseRateVisible
        {
            get { return examinationDetail.pulseRate != null; }
        }

        public int? PulseRate
        {
            get { return examinationDetail.pulseRate; }
            set { examinationDetail.pulseRate = value; }
        }

        public bool ExaminationDateisible
        {
            get { return examinationDetail.examinationDate != null; }
        }

        public DateTime? ExaminationDate
        {
            get { return examinationDetail.examinationDate; }
            set { examinationDetail.examinationDate = value; }
        }

        public Examination examination { get ; set; }

        public ExaminationViewModel(Patient patient_incoming = null, Examination examination_incoming =  null)
        {
            deatilPatient = patient_incoming;
            examination = examination_incoming;
            nSServiceClient = new NSServiceClient();
            GetExaminationsDetails();
        }

        public void GetExaminationsDetails()
        {
            examinationDetail = nSServiceClient.GetExamDetail(deatilPatient.id, examination.id).Result;
        }
    }
}
