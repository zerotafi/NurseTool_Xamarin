using NurseTool_Xamarin.Model;
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
        User myUser;

        public MainPage(User user)
		{
			InitializeComponent();
            vm = new MainViewModel();
            BindingContext = vm;
            myUser = user;

        }
        
        private void DataManagementClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.DataManagementPage());
        }

        private void SelectNewPatientClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.PatientListPage());
        }

        private void WorkFlowClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.PatientListForWorkFlow(myUser));
        }

        private void StatisticsButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.PatientDataListForStatisticPage(myUser));
        }


    }
}
