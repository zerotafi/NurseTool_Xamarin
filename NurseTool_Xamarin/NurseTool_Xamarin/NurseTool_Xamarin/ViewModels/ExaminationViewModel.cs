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

        public int? Temperature
        {
            get { return examinationDetail.temperatureValue; }
            set { examinationDetail.temperatureValue = value; }
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
