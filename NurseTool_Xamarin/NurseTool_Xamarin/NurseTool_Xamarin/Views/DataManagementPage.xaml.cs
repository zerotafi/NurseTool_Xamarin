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
	public partial class DataManagementPage : ContentPage
	{

        Examination selectedExamination;
        DataManagementViewModel vm;

        public DataManagementPage ()
		{
			InitializeComponent ();
            vm = new DataManagementViewModel();
            BindingContext = vm;
            selectedExamination = new Examination();
        }

        private void DataManagementListviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            selectedExamination = e.SelectedItem as Examination;
        }

        private void SelectNewExaminationClick(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new Views.ExamTypeListPAge(_patient));
        }
    }
}