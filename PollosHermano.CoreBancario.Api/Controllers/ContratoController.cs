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
    public partial class ContratoController : ControllerBase
    {
        readonly IContratoService _service;

        public ContratoController(IContratoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Contrato>>))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<GetContratoListModel>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetList()
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _service.GetContratoListAsync()
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Contrato>))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Contrato>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Post([FromBody] Contrato model)
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

                return Ok(new GenericResponse<Contrato>
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Contrato>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Put([FromBody] Contrato model)
        {
            try
            {
                await _service.UpdateAsync(model);

                return Ok(new GenericResponse<Contrato>
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

        
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Contrato>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity != null)
                {
                    await _service.DeleteAsync(entity);
                }

                return Ok(new GenericResponse<Contrato>
                {
                    Data = entity
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
