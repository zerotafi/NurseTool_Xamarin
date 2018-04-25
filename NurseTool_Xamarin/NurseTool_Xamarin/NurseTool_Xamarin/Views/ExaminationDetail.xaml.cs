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
	public partial class ExaminationDetail : ContentPage
	{
        ExaminationViewModel vm;

        public ExaminationDetail(Patient patient, Examination examination)
		{
			InitializeComponent ();
            vm = new ExaminationViewModel(patient, examination);
            BindingContext = vm;

        }
	}
}