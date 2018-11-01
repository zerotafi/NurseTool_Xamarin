using Microcharts;
using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.ViewModels;
using SkiaSharp;
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
    public partial class StatisticalDataPage : ContentPage
    {

        Patient myPatient = null;
        StatisticalDataViewModel vm;
        User myUser;
        List<Microcharts.Entry> entries;

        public StatisticalDataPage(Patient selectedPatient, User user)
        {
            InitializeComponent();
            myUser = user;
            myPatient = selectedPatient;
            vm = new StatisticalDataViewModel(myPatient);
            BindingContext = vm;

            InitCharts();
        }

        public void InitCharts()
        {
            if (vm.BodyTempStatLsit.Count > 0)
            {
                var entriesForBodyTempStatLsit = new List<Microcharts.Entry>();
                foreach (var item in vm.BodyTempStatLsit)
                {
                    int i = 1;
                    entriesForBodyTempStatLsit.Add(
                         new Microcharts.Entry(item)
                         {
                             Color = SKColor.Parse("#FF1943"),
                             Label = item.ToString(),
                             ValueLabel = "Value is " + item.ToString()
                         }
                        );
                    i++;
                }


                BodyTempChart.Chart = new LineChart() { Entries = entriesForBodyTempStatLsit };
            }
            if (vm.SPODataStatLsit.Count > 0)
            {
                var entriesForSPOStatLsit = new List<Microcharts.Entry>();
                foreach (var item in vm.SPODataStatLsit)
                {
                    int i = 1;
                    entriesForSPOStatLsit.Add(
                         new Microcharts.Entry(item)
                         {
                             Color = SKColor.Parse("#00BFFF"),
                             Label = item.ToString(),
                             ValueLabel = "Value is " + item.ToString()
                         }
                        );
                    i++;
                }

                BodySPOChart.Chart = new LineChart() { Entries = entriesForSPOStatLsit };
            }

            if (vm.MeanBloodPresureStatLsit.Count > 0)
            {
                var entriesForMBPStatLsit = new List<Microcharts.Entry>();
                foreach (var item in vm.MeanBloodPresureStatLsit)
                {
                    int i = 1;
                    entriesForMBPStatLsit.Add(
                         new Microcharts.Entry(item)
                         {
                             Color = SKColor.Parse("#00CED1"),
                             Label = item.ToString(),
                             ValueLabel = "Value is " + item.ToString()
                         }
                        );
                    i++;
                }

                BodyBloodChart.Chart = new LineChart() { Entries = entriesForMBPStatLsit };
            }

        }
    }
}