using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSService.Controllers
{
    [Route("api/workflow")]
    public class WorkFlowController : Controller
    {
        [HttpGet("{workflowId}")]
        public IActionResult GetWorkFlow(int workflowId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveWorkFlow()
        {
            return View();
        }

        [HttpGet("{workflowId}")]
        public IActionResult AddNewItemForWorkFlow(int workflowId)
        {
            return View();
        }
    }
}