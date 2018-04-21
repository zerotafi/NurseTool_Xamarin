using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MvvmHelpers;

namespace NurseTool_Xamarin.ViewModels
{
    class ViewModelBase  : BaseViewModel
    {
        protected Page page;
        public ViewModelBase(Page page)
        {
            this.page = page;
        }
    }
}
