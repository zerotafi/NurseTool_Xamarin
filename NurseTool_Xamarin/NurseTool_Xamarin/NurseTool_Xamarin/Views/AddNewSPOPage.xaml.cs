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
	public partial class AddNewSPOPage : ContentPage
	{
        Patient _patient;
        NewSPOViewModel vm;
		public AddNewSPOPage (Patient patient)
		{
			InitializeComponent ();
            vm = new NewSPOViewModel(patient);
            BindingContext = vm;
            _patient = patient;
        }

        private void ExamSavetClick(object sender, EventArgs e)
        {
            if (vm.SaveExamination())
            {
                Navigation.PushAsync(new Views.PatientDeatilPage(_patient));
            }
            else
            {
               //popup 
            }
        }
    }
}