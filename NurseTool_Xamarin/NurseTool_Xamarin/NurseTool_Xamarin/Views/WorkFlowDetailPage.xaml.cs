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
    public partial class WorkFlowDetailPage : ContentPage
    {
        Patient myPatient = null;
        WorkFlowDetailViewModel vm;
        User myUser;
        Model.WorkFlow myWorkFlow;

        public WorkFlowDetailPage(Patient patient, User user, Model.WorkFlow workFlow)
        {
            InitializeComponent();
            myPatient = patient;
            myUser = user;
            myWorkFlow = workFlow;

            vm = new WorkFlowDetailViewModel(user, workFlow, patient);
            BindingContext = vm;

        }


        private void AddNewWFStepClick(object sender, EventArgs e)
        { 
           if(myWorkFlow != null)
            {
              Navigation.PushAsync(new Views.WFStepSelectorPage(myPatient, myUser, myWorkFlow));
            }
        }

        private void WorkFlowStepAddCancelButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.WorkFlow(myPatient, myUser));
        }
    }
}