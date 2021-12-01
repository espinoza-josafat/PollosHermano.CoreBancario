using PollosHermano.CoreBancario.Domian.SysCore.Services;
using PollosHermano.CoreBancario.Entities.SysCore;
using PollosHermano.CoreBancario.Entities.SysCore.Models;
using PollosHermano.CoreBancario.Entities.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Api.Controllers.SysCore
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        readonly UserManager<User> _userManager;
        readonly RoleManager<Role> _roleManager;
        readonly IConfiguration _configuration;

        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public IdentityController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            IUserService userService,
            IRoleService roleService
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Role>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Roles()
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _roleService.GetAsync()
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

        [HttpGet("[action]/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Role>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Roles(Guid id)
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _roleService.GetByIdAsync(id)
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

        [HttpPost("Roles/Edit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Role>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> RolesEdit(Role model)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(model.Id);

                if (role == null)
                {
                    await _roleService.CreateAsync(model);
                }
                else
                {
                    role.Name = model.Name;
                    role.NormalizedName = model.NormalizedName;
                    role.ConcurrencyStamp = model.ConcurrencyStamp;

                    await _roleService.UpdateAsync(role);
                }

                return Ok(new GenericResponse<Role>
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

        [HttpGet("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<User>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Users()
        {
            try
            {
                return Ok(new GenericResponse
                {
                    Data = await _userService.GetAsync()
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

        [HttpGet("[action]/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<User>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Users(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                var roles = new List<Role>();

                if (user != null)
                {
                    foreach (var userRole in user.UserRoles)
                    {
                        roles.Add(userRole.Role);
                    }
                }

                return Ok(new GenericResponse
                {
                    Data = new
                    {
                        User = user,
                        Roles = roles
                    }
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

        [HttpPost("Users/Edit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> UsersEdit([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                var user = (User)null;

                if (userExists == null)
                {
                    user = new User
                    {
                        Firstname = model.Firstname,
                        FatherLastname = model.FatherLastname,
                        MotherLastname = model.MotherLastname,
                        Email = model.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = model.Username
                    };
                }
                else
                {
                    if (userExists.Id != model.Id)
                    {
                        return BadRequest(new GenericResponse
                        {
                            Status = 30,
                            Message = "El usuario ya existe",
                            Description = "El usuario ya existe"
                        });
                    }
                    else
                    {
                        userExists.Firstname = model.Firstname;
                        userExists.FatherLastname = model.FatherLastname;
                        userExists.MotherLastname = model.MotherLastname;
                        userExists.Email = model.Email;
                        userExists.UserName = model.Username;
                    }
                }

                if (user != null)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                        return NotFound(new GenericResponse
                        {
                            Status = 40,
                            Message = "No se pudo guardar los datos del usuario, es posible que la contraseña no cumpla con las caracteristicas necesarias",
                            Description = "No se pudo guardar los datos del usuario, es posible que la contraseña no cumpla con las caracteristicas necesarias"
                        });
                }
                else
                {
                    userExists.PasswordHash = _userManager.PasswordHasher.HashPassword(userExists, model.Password);

                    var result = await _userManager.UpdateAsync(userExists);

                    if (!result.Succeeded)
                        return NotFound(new GenericResponse
                        {
                            Status = 40,
                            Message = "No se pudo actualizar los datos del usuario, es posible que la contraseña no cumpla con las caracteristicas necesarias",
                            Description = "No se pudo actualizar los datos del usuario, es posible que la contraseña no cumpla con las caracteristicas necesarias"
                        });
                }

                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new Role
                    {
                        Name = model.Role,
                        NormalizedName = model.Role,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });

                var currentUser = user ?? userExists;

                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    var rolesUser = await _userManager.GetRolesAsync(currentUser);

                    if (rolesUser?.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(currentUser, rolesUser);
                    }

                    var result = await _userManager.AddToRoleAsync(currentUser, model.Role);

                    if (!result.Succeeded)
                        return NotFound(new GenericResponse
                        {
                            Status = 40,
                            Message = "No se puedo actualizar el rol del usuario",
                            Description = "No se puedo actualizar el rol del usuario"
                        });
                }

                return Ok(new GenericResponse
                {
                    Data = currentUser
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

        [HttpPost("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<JwtModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim("displayName", $"{(string.IsNullOrWhiteSpace(user.Firstname) ? string.Empty : user.Firstname.Trim())} {(string.IsNullOrWhiteSpace(user.FatherLastname) ? string.Empty : user.FatherLastname.Trim())} {(string.IsNullOrWhiteSpace(user.MotherLastname) ? string.Empty : user.MotherLastname.Trim())}".Trim()),
                        new Claim("email", user.Email),
                        new Claim("userName", user.UserName),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim("role", userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey")));

                    var token = new JwtSecurityToken(
                        issuer: null,
                        audience: null,
                        expires: DateTime.Now.AddHours(5),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new GenericResponse<JwtModel>
                    {
                        Data = new JwtModel
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Exp = token.ValidTo
                        }
                    });
                }

                return Unauthorized(new GenericResponse<JwtModel>
                {
                    Status = 30,
                    Message = "Usuario o contraseña incorectos",
                    Description = "Usuario o contraseña incorectos"
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
