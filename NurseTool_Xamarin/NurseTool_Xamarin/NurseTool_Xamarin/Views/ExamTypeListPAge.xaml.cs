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
	public partial class ExamTypeListPAge : ContentPage
	{
        ExaminationTypeListViewModel vm;
        public ExamTypeListPAge (Patient patient)
        {
			InitializeComponent ();
            vm = new ExaminationTypeListViewModel(patient);
            BindingContext = vm;
        }

        private void ExaminationTypeListviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
        }

        private void ExamTypeNextClick(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new Views.ExamTypeListPAge(_patient);
        }
    }
}