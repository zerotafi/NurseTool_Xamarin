using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class WorkFlow
    {
        [Key]
        public int WorkFlowId { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public ICollection<WorkFlowStep> WorkFlowSteps { get; set; } = new List<WorkFlowStep>();

        public string WorkFlowName { get; set; }

    }
}
