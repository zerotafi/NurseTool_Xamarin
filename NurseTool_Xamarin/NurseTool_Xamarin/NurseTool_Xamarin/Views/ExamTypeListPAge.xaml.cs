using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NurseTool_Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExamTypeListPAge : ContentPage
	{
        Patient _patient;
        ExaminationTypeListViewModel vm;
        ExamType examType;
        public ExamTypeListPAge (Patient patient)
        {
			InitializeComponent ();
            vm = new ExaminationTypeListViewModel(patient);
            BindingContext = vm;
            _patient = patient;
        }

        private void ExaminationTypeListviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            examType = e.SelectedItem as ExamType;

        }

        private void ExamTypeNextClick(object sender, EventArgs e)
        {
            if (examType != null)
            {
                if (examType.typeName == "BodyTemperature")
                {
                    Navigation.PushAsync(new Views.AddNewBodyTempPage(_patient, null, null));
                }
                if (examType.typeName == "BloodPressure")
                {
                    Navigation.PushAsync(new Views.AddNewBloodPressurePage(_patient, null, null));
                }
                if (examType.typeName == "BloodSpO2")
                {
                    Navigation.PushAsync(new Views.AddNewSPOPage(_patient, null, null));
                }
            }
        }
    }
}