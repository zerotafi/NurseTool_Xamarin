using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class NewSPOViewModel
    {
        NSServiceClient nSServiceClient;

        public Patient deatilPatient { get; set; }

        string spoValue { get; set; }

        public string SpoValue
        {
            get { return spoValue; }
            set { spoValue = value; }
        }
        public NewSPOViewModel(Patient patient_incoming = null)
        {
            nSServiceClient = new NSServiceClient();
            deatilPatient = patient_incoming;
        }
        public bool SaveExamination()
        {
            try
            {
                int _spoValue = Convert.ToInt32(SpoValue);
                return nSServiceClient.PostExamBodySPO(deatilPatient.id, _spoValue).Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
