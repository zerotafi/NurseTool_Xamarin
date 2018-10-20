using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NurseTool_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientListForWorkFlow : ContentPage
	{

        Patient selectedPatient = null;
        PatientListForWorkFlowViewModel vm;
        User myUser;

		public PatientListForWorkFlow(User user)
		{
			InitializeComponent();
            myUser = user;
            vm = new PatientListForWorkFlowViewModel(user);
            BindingContext = vm;
        }

        private void PatientLsitviewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            selectedPatient = e.SelectedItem as Patient;
        }

        private void PatientSelectClick(object sender, EventArgs e)
        {
            if (selectedPatient != null)
                Navigation.PushAsync(new Views.WorkFlow(selectedPatient, myUser));
        }

        private void PatientSelectCancelClick(object sender, EventArgs e)
        {
                Navigation.PushAsync(new MainPage(myUser));
        }
        

        private Command loadPatientsCommand;

        public Command LoadPatientsCommand
        {
            get
            {
                return loadPatientsCommand ?? (loadPatientsCommand = new Command(ExecuteLoadPatientsCommand, () =>
                {
                    return !IsBusy;
                }));
            }
        }

        private async void ExecuteLoadPatientsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadPatientsCommand.ChangeCanExecute();

            vm.RefreshList();

            IsBusy = false;
            LoadPatientsCommand.ChangeCanExecute();
        }

        private void ExecuteLoadPatients(object sender, EventArgs e)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            vm.RefreshList();

            IsBusy = false;
        }
    }
}