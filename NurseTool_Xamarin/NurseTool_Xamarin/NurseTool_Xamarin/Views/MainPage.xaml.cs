using NurseTool_Xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NurseTool_Xamarin
{
	public partial class MainPage : ContentPage
	{
        MainViewModel vm;

        public MainPage()
		{
			InitializeComponent();
            vm = new MainViewModel();
            BindingContext = vm;
        
		}

        private void SelectNewPatientClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.PatientListPage());
        }

    }
}
