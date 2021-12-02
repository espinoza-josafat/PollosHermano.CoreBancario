using PollosHermano.CoreBancario.Domian.SysCore.Processes;
using PollosHermano.CoreBancario.Domian.SysCore.Services;
using PollosHermano.CoreBancario.Entities.Models.Common;
using PollosHermano.CoreBancario.Entities.SysCore;
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
    public partial class MenuController : ControllerBase
    {
        readonly IMenuService _service;
        readonly IProcessGetMenusByUser _processGetMenusByUser;

        public MenuController(
            IMenuService service,
            IProcessGetMenusByUser processGetMenusByUser
        )
        {
            _processGetMenusByUser = processGetMenusByUser;
            _service = service;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Menu>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _service.GetAsync()
                });
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse
                {
                    Status = -1,
                    Message = exception.Message,
                    Description = exception.Message
                });
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Menu>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _service.GetByIdAsync(id)
                });
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse
                {
                    Status = -1,
                    Message = exception.Message,
                    Description = exception.Message
                });
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Menu>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Post([FromBody] Menu model)
        {
            try
            {

                if (await _service.GetByIdAsync(model.Id) == null)
                {
                    await _service.CreateAsync(model);
                }
                else
                {
                    await _service.UpdateAsync(model);
                }

                return Ok(new GenericResponse<Menu>
                {
                    Data = model
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

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Menu>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Put([FromBody] Menu model)
        {
            try
            {
                await _service.UpdateAsync(model);

                return Ok(new GenericResponse<Menu>
                {
                    Data = model
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
