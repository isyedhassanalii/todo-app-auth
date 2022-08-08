using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.DBEntities;
using TodoApp.Core.Interfaces;
using TodoApp.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly IResponseGeneric _responseGeneric;

        public LogController(ILogService logService,
                    IResponseGeneric responseGeneric)
        {
            _logService = logService;
            _responseGeneric = responseGeneric;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Log> listOfLogs = _logService.GetAll().Result;
                return Ok(_responseGeneric.Success(result: listOfLogs));
            }
            catch (Exception ex)
            {
                return BadRequest(_responseGeneric.Error(result: ex.ToString()));
            }
        }
    }
}
