using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    public class AddWorkFlowViewModel
    {
        NSServiceClient nSServiceClient;
        public User myUser = new User();

        public string WFName { get; set; }

        public WorkFlow SelectedWorkFlow { get; set; }

        public Patient deatilPatient { get; set; }
        public string Name
        {
            get { return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }

        public AddWorkFlowViewModel(User user, Patient patient_incoming = null)
        {
            myUser = user;
            deatilPatient = patient_incoming;
            nSServiceClient = new NSServiceClient();
        }

        public bool CreatNewWorkFlow()
        {
            return nSServiceClient.CreateNewWF(myUser, deatilPatient.id, WFName).Result;
        }
    }
}
