using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NurseTool_Xamarin.ViewModels;

namespace NurseTool_Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientListPage : ContentPage
	{
        Patient selectedPatient = null;

        PatientListViewModelcs vm;
        public PatientListPage ()
		{
			InitializeComponent ();
            vm = new PatientListViewModelcs();
            BindingContext = vm;
        }

        private void PatientLsitviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            selectedPatient = e.SelectedItem as Patient;
         
            Navigation.PushAsync(new Views.PatientDeatilPage(selectedPatient));

        }

    }
}