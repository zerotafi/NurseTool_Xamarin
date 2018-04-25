using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using NurseTool_Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class PatientListViewModelcs : BaseViewModel
    {
       NSServiceClient nSServiceClient;
       public  ObservableCollection<Patient> patientList;

        public PatientListViewModelcs()
        {
            nSServiceClient = new NSServiceClient();
            patientList = new ObservableCollection<Patient>();
            GetPatients();
        }

        public ObservableCollection<Patient> PatietnList
        {
            get { GetPatients(); return patientList; }
            set { patientList = value; }
        }

        public void GetPatients()
        {
           var patientListToAdd = nSServiceClient.GetPatients().Result;
           patientListToAdd.ForEach(x => patientList.Add(x));
        }

    }
}
