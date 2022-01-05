using Evaluation.Portal.API.Models;
using Evaluation.Portal.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("GetReport")]
        public ActionResult<Report> GetReport([FromBody] ReportFilter filter)
        {
            var emp = _reportService.GetReport(filter);

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }
    }
}
