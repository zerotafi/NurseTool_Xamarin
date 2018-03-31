using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Controllers
{
    [Route("api/patients")]
    public class PatientController : Controller
    {
        [HttpGet()]
        public IActionResult GetAllPatient()
        {
            return Ok(PatientDataStore.Current.Patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetPatient(int id)
        {
            var patient = PatientDataStore.Current.Patients.FirstOrDefault(x => x.Id == id);

            if (patient == null) { return NotFound(); }
            return Ok(patient);
        }

    }
}
