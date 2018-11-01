using Microcharts;
using MvvmHelpers;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class StatisticalDataViewModel : BaseViewModel
    {
        NSServiceClient nSServiceClient;
        public ObservableCollection<int> statisticalDataList;
        private List<Examination> examinationList;
        public Chart ChartData;

        public List<int> BodyTempStatLsit = new List<int>();
        public List<int> SPODataStatLsit = new List<int>();
        public List<int> MeanBloodPresureStatLsit = new List<int>();

        public string Name
        {
            get { return deatilPatient.name; }
            set { deatilPatient.name = value; }
        }

        public ObservableCollection<int> StatisticalDataList
        {
            get { GetStatisticalData(); return statisticalDataList; }
            set { statisticalDataList = value; }
        }
        

        public Patient deatilPatient { get; set; }
        public StatisticalDataViewModel(Patient patient_incoming = null)
        {
            deatilPatient = patient_incoming;
            statisticalDataList = new ObservableCollection<int>();
            nSServiceClient = new NSServiceClient();
            GetExaminations();
        }
        public void GetStatisticalData()
        {
            statisticalDataList.Add(6);
            statisticalDataList.Add(8);
            statisticalDataList.Add(19);
            statisticalDataList.Add(12);
        }

        public void GetExaminations()
        {
           examinationList = nSServiceClient.GetExamList(deatilPatient.id).Result;
            BodyTempStatLsit.Clear();
            SPODataStatLsit.Clear();
            MeanBloodPresureStatLsit.Clear();
            foreach (var item in examinationList)
            {
                if (item != null)
                {
                    if (item.examinationType == "SpO2")
                    {
                        var itemToAdd = nSServiceClient.GetExamDetail(deatilPatient.id, item.id).Result.spoValue.Value;
                        SPODataStatLsit.Add(itemToAdd);
                    }
                    if (item.examinationType == "BloodPressure")
                    {
                        var itemToAdd = nSServiceClient.GetExamDetail(deatilPatient.id, item.id).Result.meanBloodPressure.Value;
                        MeanBloodPresureStatLsit.Add(itemToAdd);
                    }
                    if (item.examinationType == "Body temperature")
                    {
                        var itemToAdd = nSServiceClient.GetExamDetail(deatilPatient.id, item.id).Result.temperatureValue.Value;
                        BodyTempStatLsit.Add(itemToAdd);
                    }
                }        
            }
        }

    }
}
