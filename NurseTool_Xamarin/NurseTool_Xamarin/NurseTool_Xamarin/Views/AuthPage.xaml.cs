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
	public partial class AuthPage : ContentPage
	{
         AuthViewModel vm;
		public AuthPage ()
		{
			InitializeComponent ();
            vm = new AuthViewModel();
            BindingContext = vm;
        }

        private void AuthClick(object sender, EventArgs e)
        {
            if (vm.Auth())
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                //popup 
            }
        }
    }
}