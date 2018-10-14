using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    public class PatientListForWorkFlowViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;
        public ObservableCollection<Patient> patientList;
        User myUser;

        public PatientListForWorkFlowViewModel(User user)
        {
            nSServiceClient = new NSServiceClient();
            patientList = new ObservableCollection<Patient>();
            myUser = user;
        }

        public ObservableCollection<Patient> PatietnList
        {
            get { GetPatients(); return patientList; }
            set { patientList = value; }
        }

        public void RefreshList()
        {
            patientList = new ObservableCollection<Patient>();
        }

        public void GetPatients()
        {
            var patientListToAdd = nSServiceClient.GetPatients().Result;
            patientListToAdd.ForEach(x => patientList.Add(x));
        }

  
    }
}
