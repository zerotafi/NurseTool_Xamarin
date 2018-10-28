using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class StatisticalDataViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;
        public ObservableCollection<int> statisticalDataList;

        public string Name
        {
            get { return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }

        public ObservableCollection<int> StatisticalDataList
        {
            get { GetExaminations(); return statisticalDataList; }
            set { statisticalDataList = value; }
        }

        public Patient deatilPatient { get; set; }
        public StatisticalDataViewModel(Patient patient_incoming = null)
        {
            deatilPatient = patient_incoming;
            statisticalDataList = new ObservableCollection<int>();
            nSServiceClient = new NSServiceClient();
        }
        public void GetExaminations()
        {
            //var examinationListToAdd = nSServiceClient.GetExamList(deatilPatient.id).Result;
            //examinationListToAdd.ForEach(x => examinationList.Add(x));
        }
    }
}
