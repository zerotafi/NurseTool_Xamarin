using NSService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Models
{
    public class WorkFlowDTO
    {
        public int WorkFlowId { get; set; }
        public string Username { get; set; }
        public int PatientID { get; set; }
        public string WorkFlowName { get; set; }
    }
}
