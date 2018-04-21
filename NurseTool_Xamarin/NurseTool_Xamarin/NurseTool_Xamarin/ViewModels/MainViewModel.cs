using MvvmHelpers;
using System;
using Xamarin.Forms;

namespace NurseTool_Xamarin.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public string SelectPatientText { get; set; }

        public string NewPatientText { get; set; }

        public MainViewModel() 
        {
            // Temp.
            SelectPatientText = "Select patient";

            NewPatientText = "New patient";
        }
    }
}
