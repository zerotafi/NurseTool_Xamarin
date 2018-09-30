using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class Examination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [ForeignKey("WorkFlowId")]
        public WorkFlow WorkFlow { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string ExaminationType { get; set; }

        public string Value { get; set; }

        public bool Archived { get; set; }
    }
}
