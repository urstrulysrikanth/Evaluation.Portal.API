using Evaluation.Portal.API.Models;
using Evaluation.Portal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Evaluation.Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateService _candidateService;

        public CandidateController(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public ActionResult<List<Candidate>> Get()
        {
            List<Candidate> candidates;
            try
            {
                candidates = _candidateService.Get();

                if (candidates == null)
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

            return candidates;
        }

        public ActionResult<Candidate> Get(string id)
        {
            var candidate = _candidateService.Get(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }


        [HttpPost]
        public void Post([FromBody] Candidate candidate)
        {
            try
            {
                _candidateService.InsertCandidate(candidate);
            }
            catch (Exception ex)
            {
            }
        }

        [HttpPost("InsertCandidates")]
        public void Post([FromBody] List<Candidate> candidates)
        {
            try
            {
                _candidateService.InsertCandidates(candidates);
            }
            catch (Exception ex)
            {
            }
        }


        [HttpPut]
        public void Put([FromBody] Candidate candidate)
        {
            try
            {
                //  _candidateService.UpdateUser(user);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpDelete("{candidateId}")]
        public void Delete(string candidateId)
        {
            try
            {
                _candidateService.DeleteCandidate(candidateId);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpGet("GetNonRejectedCandidates")]
        public ActionResult<List<Candidate>> GetNonRejectedCandidates()
        {
            List<Candidate> candidates;
            try
            {
                candidates = _candidateService.GetNonRejectedCandidates();

                if (candidates == null)
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
            return candidates;
        }

        [HttpGet("GetCandidatesByPanelSetupStatus")]
        public ActionResult<List<Candidate>> GetCandidatesByPanelSetupStatus()
        {
            List<Candidate> candidates;
            try
            {
                candidates = _candidateService.GetCandidatesByPanelSetupStatus();

                if (candidates == null)
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
            return candidates;
        }


        [HttpPost("InsertCandidateHistory")]
        public void InsertCandidateHistory([FromBody] CandidateHistory candidateHistory)
        {
            try
            {
                _candidateService.InsertCandidateHistory(candidateHistory);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
