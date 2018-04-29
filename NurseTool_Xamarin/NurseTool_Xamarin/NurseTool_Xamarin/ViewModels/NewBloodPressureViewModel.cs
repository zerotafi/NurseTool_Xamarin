using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class NewBloodPressureViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;

        public Patient deatilPatient { get; set; }

        string diastolicValue { get; set; }

        public string DiastolicValue
        {
            get { return diastolicValue; }
            set { diastolicValue = value; }
        }

        string systolicValue { get; set; }

        public string SystolicValue
        {
            get { return systolicValue; }
            set { systolicValue = value; }
        }

        string meanBloodPressure { get; set; }

        public string MeanBloodPressure
        {
            get { return meanBloodPressure; }
            set { meanBloodPressure = value; }
        }

        string pulseRate { get; set; }

        public string PulseRate
        {
            get { return pulseRate; }
            set { pulseRate = value; }
        }

        public NewBloodPressureViewModel(Patient patient_incoming = null)
        {
            nSServiceClient = new NSServiceClient();
            deatilPatient = patient_incoming;
        }

        public bool SaveExamination()
        {
            try
            {
                int diastolicValue = Convert.ToInt32(DiastolicValue);
                int systolicValue = Convert.ToInt32(SystolicValue);
                int meanBloodPressure = Convert.ToInt32(MeanBloodPressure);
                int pulseRate = Convert.ToInt32(PulseRate);
                return nSServiceClient.PostExamBodyBloodPressure(deatilPatient.id, diastolicValue, systolicValue, meanBloodPressure, pulseRate).Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
