using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class WorkFlowStep
    {
        [Key]
        public int WorkFlowStepId { get; set; }

        public string WorkFlowStepName { get; set; }
    }
}