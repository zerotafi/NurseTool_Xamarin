using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public int ExternalId { get; set; }

        public string OriginalHL7Message { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public bool Archived { get; set; }

        public ICollection<Examination> Examinations { get; set; } = new List<Examination>();
    }
}
