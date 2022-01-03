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
    public class PanelController : ControllerBase
    {
        private readonly PanelService _panelService;

        public PanelController(PanelService panelService)
        {
            _panelService = panelService;
        }
        [HttpGet]
        public ActionResult<List<Panel>> Get()
        {
            List<Panel> panels;
            try
            {
                panels = _panelService.Get();

                if (panels == null)
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

            return panels;
        }

        public ActionResult<Panel> Get(string id)
        {
            var panel = _panelService.Get(id);

            if (panel == null)
            {
                return NotFound();
            }

            return panel;
        }


        [HttpPost]
        public void Post([FromBody] Panel panel)
        {
            try
            {
                _panelService.InsertPanel(panel);
            }
            catch (Exception ex)
            {
            }
        }


        [HttpPut]
        public void Put([FromBody] Panel panel)
        {
            try
            {
                _panelService.UpdatePanel(panel);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpDelete("{panelId}")]
        public void Delete(string panelId)
        {
            try
            {
                _panelService.DeletePanel(panelId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
