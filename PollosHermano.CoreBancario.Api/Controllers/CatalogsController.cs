using PollosHermano.MicroFramework.Tools.Exceptions;
using PollosHermano.CoreBancario.Application.Catalogs;
using PollosHermano.CoreBancario.Entities.Catalogs.Models;
using PollosHermano.CoreBancario.Entities.Models.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public partial class CatalogsController : ControllerBase
    {
        readonly IWebHostEnvironment _webHostEnvironment;
        PluginLoadContext _loadContext;

        public CatalogsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("run")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Run([FromBody] RunModel model)
        {
            try
            {
                if (model.Catalog == null)
                    throw new DataValidationException("El modelo 'Catalog' no puede ser nulo");

                return Ok(new GenericResponse
                {
                    Data = await ExecuteAsync(model.Run, model.Catalog)
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
