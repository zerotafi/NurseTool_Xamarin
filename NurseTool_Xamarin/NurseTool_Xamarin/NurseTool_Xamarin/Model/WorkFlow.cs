using System;
using System.Collections.Generic;
using System.Text;

namespace NurseTool_Xamarin.Model
{
    public class WorkFlow
    {
        public int workFlowId { get; set; }

        public string username { get; set; }

        public Patient patient { get; set; }

        public string workFlowName { get; set; }
    }
}
