using PollosHermano.CoreBancario.Domian.Core.Services;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using PollosHermano.CoreBancario.Entities.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Cliente>>))]
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
        
        [HttpGet("List")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<GetClienteListModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetList()
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _service.GetClienteListAsync()
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Cliente>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Get(int id)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Cliente>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Post([FromBody] Cliente model)
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

                return Ok(new GenericResponse<Cliente>
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Cliente>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Put([FromBody] Cliente model)
        {
            try
            {
                await _service.UpdateAsync(model);

                return Ok(new GenericResponse<Cliente>
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
        
    }
}
