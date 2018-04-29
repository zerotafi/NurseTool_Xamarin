using MvvmHelpers;
using NurseTool_Xamarin.Common;
using NurseTool_Xamarin.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{

    class ExaminationTypeListViewModel : BaseViewModel
    {
        public Patient deatilPatient { get; set; }

        public ObservableCollection<ExamType> examinationTypeList;

        public ObservableCollection<ExamType> ExaminationTypeList
        {
            get {return examinationTypeList; }
            set { examinationTypeList = value; }
        }

        public ExaminationTypeListViewModel(Patient patient_incoming = null)
        {
            examinationTypeList = new ObservableCollection<ExamType>();
            deatilPatient = patient_incoming;
            GetExaminationTypes();
        }

        public string Name
        {
            get { return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }

        public void GetExaminationTypes()
        {
            foreach (ExaminationType item in Enum.GetValues(typeof(ExaminationType)))
            {
                ExamType examinationType = new ExamType() { typeName = item.ToString() };
                examinationTypeList.Add(examinationType);
            }
        }
    }
}
