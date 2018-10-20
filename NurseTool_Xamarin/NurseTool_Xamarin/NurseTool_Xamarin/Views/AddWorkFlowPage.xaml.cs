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
	public partial class AddWorkFlowPage : ContentPage
	{
        Patient myPatient = null;
        AddWorkFlowViewModel vm;
        User myUser;

        public AddWorkFlowPage(Patient patient, User user)
        {
			InitializeComponent ();
            myUser = user;
            myPatient = patient;
            vm = new AddWorkFlowViewModel(myUser, myPatient);
            BindingContext = vm;
           
        }

        private void WFCancelClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.WorkFlow(myPatient, myUser));
        }

        private void WFCreateClick(object sender, EventArgs e)
        {
            if (vm.CreatNewWorkFlow())
            {
                Navigation.PushAsync(new Views.WorkFlow(myPatient, myUser));
            }
            else

            {
                // Show error
            }

        }
        

    }
}