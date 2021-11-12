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
    public class SucursalController : ControllerBase
    {
        readonly ISucursalService _service;

        public SucursalController(ISucursalService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Sucursal>>))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<GetSucursalListModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetList()
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _service.GetSucursalListAsync()
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Sucursal>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Get(byte id)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Sucursal>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Post([FromBody] Sucursal model)
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

                return Ok(new GenericResponse<Sucursal>
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Sucursal>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Put([FromBody] Sucursal model)
        {
            try
            {
                await _service.UpdateAsync(model);

                return Ok(new GenericResponse<Sucursal>
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
