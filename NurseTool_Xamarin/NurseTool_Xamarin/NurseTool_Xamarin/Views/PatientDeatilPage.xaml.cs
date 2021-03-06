﻿using NurseTool_Xamarin.Model;
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
	public partial class PatientDeatilPage : ContentPage
	{
        Patient _patient;
        Examination selectedExamination;
        PatientDeatilViewModel vm;

        public  PatientDeatilPage (Patient patient)
		{
			InitializeComponent ();
            vm = new PatientDeatilViewModel(patient);
            BindingContext = vm;
            selectedExamination = new Examination();
            _patient = patient;
        }

        private void ExaminationListviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            selectedExamination = e.SelectedItem as Examination;
            Navigation.PushAsync(new Views.ExaminationDetail(_patient,selectedExamination));
        }

        private void SelectNewExaminationClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.ExamTypeListPAge(_patient));
        }

    }
}