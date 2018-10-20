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
	public partial class WFStepSelectorPage : ContentPage
	{
        Patient myPatient = null;
        WFStepSelectorViewModel vm;
        User myUser;
        Model.WorkFlow myWorkFlow;
        public WFStepSelectorPage (Patient patient, User user, Model.WorkFlow workFlow)
		{
			InitializeComponent ();
            myPatient = patient;
            myUser = user;
            myWorkFlow = workFlow;

            vm = new WFStepSelectorViewModel(user, workFlow, patient);
            BindingContext = vm;
        }
        private void WFStepListviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            vm.MywfStepName = (e.SelectedItem as WorkFlowStep).workFlowStepName;
        }

        private void WorkFlowStepAddButtonClicked(object sender, EventArgs e)
        {
            if (vm.AddWFStep())
            {
                Navigation.PushAsync(new Views.WorkFlowDetailPage(myPatient, myUser, myWorkFlow));
            }
            else
            {
                // Show Error
            }
        }


        private void WfStepCancelButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.WorkFlowDetailPage(myPatient, myUser, myWorkFlow));
        }

    }
}