using MvvmHelpers;
using NurseTool_Xamarin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    public class PatientDeatilViewModel : BaseViewModel
    {
        public string Name
        {
            get {  return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }
        public Patient deatilPatient { get; set; }
        public PatientDeatilViewModel(Patient patient_incoming = null)
        {
            deatilPatient = patient_incoming;
        }

    }
}
