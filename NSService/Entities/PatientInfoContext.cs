using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class PatientInfoContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Examination> Examinations { get; set; }

        public PatientInfoContext(DbContextOptions<PatientInfoContext> options) : base(options)
        {
            Database.Migrate();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
