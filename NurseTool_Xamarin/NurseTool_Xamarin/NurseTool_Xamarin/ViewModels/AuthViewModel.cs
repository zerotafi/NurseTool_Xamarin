using NurseTool_Xamarin.Model;
using NurseTool_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.ViewModels
{
    class AuthViewModel
    {
        NSServiceClient nSServiceClient;

        public User User;

        public string Username
        {
            get { return User.Username; }
            set { User.Username = value; }
        }

        public string Password
        {
            get { return User.Password; }
            set { User.Password = value; }
        }

        public AuthViewModel()
        {
            User = new User();
            nSServiceClient = new NSServiceClient();
        }

        public bool Auth()
        {
            var test = nSServiceClient.AuthUser(User);
            //return nSServiceClient.AuthUser(User).Result;

            return true;
        }
    }
}
