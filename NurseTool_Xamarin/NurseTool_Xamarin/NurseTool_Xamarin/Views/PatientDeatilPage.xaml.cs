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
	public partial class PatientDeatilPage : ContentPage
	{
         PatientDeatilViewModel vm;

        public  PatientDeatilPage (Patient patient)
		{
			InitializeComponent ();
            vm = new PatientDeatilViewModel(patient);
            BindingContext = vm;
        }

        private void ExaminationListviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
        }

    }
}