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
	public partial class WorkFlow : ContentPage
	{
        Patient myPatient = null;
        WorkFlowViewModel vm;
        User myUser;

        public WorkFlow(Patient patient, User user)
		{
			InitializeComponent ();
            vm = new WorkFlowViewModel(user, patient);
            BindingContext = vm;
            myUser = user;
            myPatient = patient;
        }


        private void WorkFlowListviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            vm.SelectedWorkFlow = e.SelectedItem as Model.WorkFlow;
        }

        private void SelectNewWFDeatilClick(object sender, EventArgs e)
        {
            if(vm.SelectedWorkFlow != null)
            Navigation.PushAsync(new Views.WorkFlowDetailPage(myPatient, myUser, vm.SelectedWorkFlow));
        }
    }
}