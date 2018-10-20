using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    public class WFStepSelectorViewModel
    {
        NSServiceClient nSServiceClient;
        public WorkFlow myWorkFlow;
        public User myUser = new User();
        public ObservableCollection<WorkFlowStep> myWorkFlowStep;

        public string MywfStepName { get; set; }

        public Patient deatilPatient { get; set; }
        public string Name
        {
            get { return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }

        public ObservableCollection<WorkFlowStep> WorkFlowStep
        {
            get { GetWorkFlowSteps(); return myWorkFlowStep; }
            set { myWorkFlowStep = value; }
        }

        public WFStepSelectorViewModel(User user, WorkFlow workFlow, Patient patient_incoming = null)
        {
            myUser = user;
            myWorkFlow = workFlow;
            deatilPatient = patient_incoming;
            myWorkFlowStep = new ObservableCollection<WorkFlowStep>();
            nSServiceClient = new NSServiceClient();
            GetWorkFlowSteps();
        }

        public bool AddWFStep()
        {
           return nSServiceClient.AddWorkFlowStepToWorkFlow(myWorkFlow.workFlowId.Value, MywfStepName).Result;
        }

        public void GetWorkFlowSteps()
        {
            myWorkFlowStep.Clear();
            Model.WorkFlowStep wfStep = new Model.WorkFlowStep();
            wfStep.workFlowStepName = "BloodPressure";
            myWorkFlowStep.Add(wfStep);

            Model.WorkFlowStep wfStep1 = new Model.WorkFlowStep();
            wfStep1.workFlowStepName = "Body temperature";
            myWorkFlowStep.Add(wfStep1);

            Model.WorkFlowStep wfStep2 = new Model.WorkFlowStep();
            wfStep2.workFlowStepName = "SpO2";
            myWorkFlowStep.Add(wfStep2);

        }
    }
}
