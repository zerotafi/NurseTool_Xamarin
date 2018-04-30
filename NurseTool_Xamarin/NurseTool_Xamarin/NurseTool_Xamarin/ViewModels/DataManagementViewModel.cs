using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace NurseTool_Xamarin.ViewModels
{
    class DataManagementViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;
        public ObservableCollection<Examination> examinationList;
        List<Patient> Patients;
        public ObservableCollection<Examination> ExaminationList
        {
            get { GetExaminations(); return examinationList; }
            set { examinationList = value; }
        }

        public DataManagementViewModel()
        {
            examinationList = new ObservableCollection<Examination>();
            nSServiceClient = new NSServiceClient();
            Patients = nSServiceClient.GetPatients().Result;

        }
        public void GetExaminations()
        {
            List<Examination> allExam = new List<Examination>();
            foreach (var item in Patients)
            {
                List<Examination> examinationListToAdd = nSServiceClient.GetExamList(item.id).Result;
                var unArchiveexaminationListToAdd = examinationListToAdd.Where(x => x.archived == false).ToList();
                unArchiveexaminationListToAdd.ForEach(x => allExam.Add(x));
            }
            allExam.ForEach(x => examinationList.Add(x));
        }
    }
}
