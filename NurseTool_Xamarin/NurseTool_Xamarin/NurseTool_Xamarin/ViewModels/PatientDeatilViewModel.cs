using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    public class PatientDeatilViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;
        public ObservableCollection<Examination> examinationList;

        public string Name
        {
            get {  return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }

        public ObservableCollection<Examination> ExaminationList
        {
            get { GetExaminations(); return examinationList; }
            set { examinationList = value; }
        }

        public Patient deatilPatient { get; set; }
        public PatientDeatilViewModel(Patient patient_incoming = null)
        {
            deatilPatient = patient_incoming;
            examinationList = new ObservableCollection<Examination>();
            nSServiceClient = new NSServiceClient();
            GetExaminations();
        }
        public void GetExaminations()
        {
            var examinationListToAdd = nSServiceClient.GetExamList(deatilPatient.id).Result;
            examinationListToAdd.ForEach(x => examinationList.Add(x));
        }
    }
}
