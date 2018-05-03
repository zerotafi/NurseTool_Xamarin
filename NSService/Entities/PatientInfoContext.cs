using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Entities
{
    public class PatientInfoContext : DbContext
    {
        private NLog.Logger _logger;
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<BloodPressureData> BloodPressureData { get; set; }
        public DbSet<BodyTemperatureData> BodyTemperatureData { get; set; }
        public DbSet<SpOData> SpOData { get; set; }
        public DbSet<User> Users { get; set; }



        public PatientInfoContext(DbContextOptions<PatientInfoContext> options) : base(options)
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _logger.Log(NLog.LogLevel.Info, "PatientInfoContext  created.");
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
