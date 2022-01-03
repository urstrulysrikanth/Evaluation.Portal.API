using Evaluation.Portal.API.Models;
using Evaluation.Portal.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace Evaluation.Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngagementController : ControllerBase
    {
        private readonly EngagementService _engagementService;

        public EngagementController(EngagementService engagementService)
        {
            _engagementService = engagementService;
        }

        [HttpGet]
        public ActionResult<List<Engagement>> Get()
        {
            List<Engagement> engagements;
            try
            {
                engagements = _engagementService.Get();

                if (engagements == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return engagements;
        }

        public ActionResult<Engagement> Get(string id)
        {
            var emp = _engagementService.Get(id);

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }


        [HttpPost]
        public void Post([FromBody] Engagement engagement)
        {
            try
            {
                _engagementService.InsertEngagement(engagement);
            }
            catch (Exception ex)
            {
            }
        }


        [HttpPut]
        public void Put([FromBody] Engagement engagement)
        {
            try
            {
                _engagementService.UpdateEngagement(engagement);
            }
            catch (Exception ex)
            {
            }
        }

        [HttpDelete("{engagementId}")]
        public void Delete(string engagementId)
        {
            try
            {
                _engagementService.DeleteEngagement(engagementId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
