using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace NurseTool_Xamarin.ViewModels
{
	public class WorkFlowDetailViewModel : ContentPage
	{
        NSServiceClient nSServiceClient;
        public WorkFlow myWorkFlow;
        public User myUser = new User();
        public ObservableCollection<WorkFlowStep> myWorkFlowStep;

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

        public WorkFlowDetailViewModel (User user, WorkFlow workFlow, Patient patient_incoming = null )
		{
            myUser = user;
            myWorkFlow = workFlow;
            deatilPatient = patient_incoming;
            myWorkFlowStep = new ObservableCollection<WorkFlowStep>();
            nSServiceClient = new NSServiceClient();
            GetWorkFlowSteps();
        }

        public void GetWorkFlowSteps()
        {
            myWorkFlowStep.Clear();
            var workFlowStepListLoc = nSServiceClient.GetWorkFlowSteps(myWorkFlow.workFlowId.Value).Result;
            workFlowStepListLoc.ForEach(x => myWorkFlowStep.Add(x));
        }
    }
}