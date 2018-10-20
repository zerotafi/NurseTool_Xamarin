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
    public partial class AddNewBodyTempPage : ContentPage
    {
        NewBodyTempViewModel vm;
        Patient _patient;
        List<WorkFlowStep> wfSteps = null;
        User myUser;

        public AddNewBodyTempPage(Patient patient, List<WorkFlowStep> wfsteps, User user)
        {
            InitializeComponent();
            vm = new NewBodyTempViewModel(patient);
            BindingContext = vm;
            _patient = patient;
            wfSteps = wfsteps;
            myUser = user;
        }

        private void ExamSavetClick(object sender, EventArgs e)
        {
            if (wfSteps == null)
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
            else
            {
                vm.SaveExamination();
                if (wfSteps.Count == 0)
                {
                    Navigation.PushAsync(new Views.WorkFlow(_patient, myUser));
                }
                else
                {
                    var wfStepLsit = wfSteps;
                    WorkFlowStep wfStep = wfStepLsit.FirstOrDefault();
                    wfStepLsit.Remove(wfStep);
                    if (wfStep != null)
                    {
                        switch (wfStep.workFlowStepName)
                        {
                            case "BloodPressure":
                                Navigation.PushAsync(new Views.AddNewBloodPressurePage(_patient, wfStepLsit, myUser));
                                break;
                            case "Body temperature":
                                Navigation.PushAsync(new Views.AddNewBodyTempPage(_patient, wfStepLsit, myUser));
                                break;
                            case "SpO2":
                                Navigation.PushAsync(new Views.AddNewSPOPage(_patient, wfStepLsit, myUser));
                                break;
                        }
                    }
                    else
                    {
                        Navigation.PushAsync(new Views.WorkFlow(_patient, myUser));
                    }
                }
            }
        }
    }
}