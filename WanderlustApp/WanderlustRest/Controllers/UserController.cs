using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WanderlustResource.Backend;
using WanderlustService.DataTransferObject.Entities.User;
using WanderlustService.Facade.Users;

namespace WanderlustRest.Controllers
{
    /// <summary>
    /// A controller responsible for handling actions on users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// A facade for user operations
        /// </summary>
        private readonly IUserFacade userFacade;

        /// <summary>
        /// A class used for accessing configuration information
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userFacade">A facade for user operations</param>
        /// <param name="configuration">Configuration</param>
        public UserController(IUserFacade userFacade, IConfiguration configuration)
        {
            this.userFacade = userFacade;
            this.configuration = configuration;
        }

        /// <summary>
        /// Attempts to log in a user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>True if the login was successful. Otherwise false</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            if (ModelState.IsValid)
            {
                bool result = await userFacade.AuthentizeAsync(userDto);
                if (result)
                {
                    var token = await GenerateTokenAsync(userDto.Username);
                    return new ObjectResult(token);
                }
                return BadRequest(Warning.WLE004);
            }

            var failedValidations = ExtractErrorMessagesFromModelState();
            return BadRequest(failedValidations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UserUpdateDto userDto)
        {
            if (ModelState.IsValid)
            {
                await userFacade.UpdateAsync(userDto);
                return Ok();
            }
            return BadRequest(ExtractErrorMessagesFromModelState());
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="userDto">User DTO containing information needed for a registration</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            if (ModelState.IsValid)
            {
                await userFacade.RegisterAsync(userDto);
                return Ok();
            }

            var failedValidations = ExtractErrorMessagesFromModelState();
            return BadRequest(failedValidations);
        }

        /// <summary>
        /// Generates a new JWT token
        /// </summary>
        /// <param name="username">User's name</param>
        /// <returns>Newly generated JavaScript Web Token (JWT)</returns>
        private async Task<dynamic> GenerateTokenAsync(string username)
        {
            var user = await userFacade.FindByUsernameAsync(username);

            IList<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(4)).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWTSecretKey")));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload(claims);
            var token = new JwtSecurityToken(header, payload);

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = username
            };

            return output;
        }

        /// <summary>
        /// Extracts error messages from model state
        /// </summary>
        /// <returns>A collection of error messages in a model state</returns>
        private IEnumerable<string> ExtractErrorMessagesFromModelState()
        {
            return ModelState.Values.SelectMany(e => e.Errors.Select(error => error.ErrorMessage));
        }
    }
}
