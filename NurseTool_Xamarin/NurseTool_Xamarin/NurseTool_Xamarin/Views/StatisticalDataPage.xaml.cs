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
	public partial class StatisticalDataPage : ContentPage
	{

        Patient myPatient = null;
        StatisticalDataViewModel vm;
        User myUser;

        public StatisticalDataPage (Patient selectedPatient, User user)
		{
			InitializeComponent ();
            myUser = user;
            myPatient = selectedPatient;
            vm = new StatisticalDataViewModel(myPatient);
            BindingContext = vm;

        }
	}
}