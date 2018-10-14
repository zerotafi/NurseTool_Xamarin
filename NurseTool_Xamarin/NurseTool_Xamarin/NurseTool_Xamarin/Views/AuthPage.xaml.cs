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
        bool lockobj = false;
		public AuthPage ()
		{
			InitializeComponent ();
            lockobj = false;
            vm = new AuthViewModel();
            BindingContext = vm;
        }

        private void AuthClick(object sender, EventArgs e)
        {
            if (vm.Auth())
            {
                if (!lockobj)
                {
                    Navigation.PushAsync(new MainPage(vm.User));
                    lockobj = true;
                }
            }
            else
            {
                //popup 
            }
        }
    }
}