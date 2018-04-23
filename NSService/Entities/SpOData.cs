using NSService.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class SpOData: IExaminationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ExaminationId { get; set; }

        [ForeignKey("ExaminationId")]
        public Examination Examination { get; set; }

        public int SPOValue { get; set; }
    }
}
