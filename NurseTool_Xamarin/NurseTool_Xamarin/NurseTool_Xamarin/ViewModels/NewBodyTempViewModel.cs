using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class NewBodyTempViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;

        public Patient deatilPatient { get; set; }

        string temperatureValue { get; set; }

        public string TemperatureValue
        {
            get { return temperatureValue; }
            set { temperatureValue = value; }
        }

        public NewBodyTempViewModel(Patient patient_incoming = null)
        {
            nSServiceClient = new NSServiceClient();
            deatilPatient = patient_incoming;
        }

        public bool SaveExamination()
        {
            try
            {
              int temperatureValue = Convert.ToInt32(TemperatureValue);
              return nSServiceClient.PostExamBodyTemp(deatilPatient.id, temperatureValue).Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
