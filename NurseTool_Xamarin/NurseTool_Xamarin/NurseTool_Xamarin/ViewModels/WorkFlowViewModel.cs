using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    public class WorkFlowViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;
        public ObservableCollection<WorkFlow> workFlowList;
        public User myUser = new User();

        public ObservableCollection<WorkFlow> WorkFlowList
        {
            get { GetWorkFlows(); return workFlowList; }
            set { workFlowList = value; }
        }

        public Patient deatilPatient { get; set; }
        public string Name
        {
            get { return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }

        public WorkFlowViewModel(User user, Patient patient_incoming = null)
        {
            myUser = user;
            deatilPatient = patient_incoming;
            workFlowList = new ObservableCollection<WorkFlow>();
            nSServiceClient = new NSServiceClient();
        }
        public void GetWorkFlows()
        {
             var workFlowList = nSServiceClient.GetWorkFlow(myUser, deatilPatient.id).Result;
            //examinationListToAdd.ForEach(x => examinationList.Add(x));
        }

    }
}
