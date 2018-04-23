using Microsoft.AspNetCore.Mvc;
using NSService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSService.Controllers
{
    public class TestController : Controller
    {
        public PatientInfoContext _ctx;

        public TestController(PatientInfoContext ctx)

        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDDatabase()
        {
            return Ok();
        }
    }
}
