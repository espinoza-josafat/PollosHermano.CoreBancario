using PollosHermano.CoreBancario.Domian.SysCore.Processes;
using PollosHermano.CoreBancario.Entities.Models.Common;
using PollosHermano.CoreBancario.Entities.SysCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Api.Controllers.SysCore
{
    [Route("SysCore/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        readonly IProcessGetMenusByUser _processGetMenusByUser;
        
        public MenuController(IProcessGetMenusByUser processGetMenusByUser)
        {
            _processGetMenusByUser = processGetMenusByUser;
        }

        [HttpGet("[action]/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<MenuModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetMenuByUser(Guid userId)
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _processGetMenusByUser.ExecuteAsync(userId)
                });
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse
                {
                    Status = -1,
                    Message = exception.Message,
                    Description = exception.StackTrace,
                });
            }
        }
    }
}
