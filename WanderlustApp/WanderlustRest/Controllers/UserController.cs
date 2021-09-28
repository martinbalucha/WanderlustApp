using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderlustService.DataTransferObject.Entities.User;
using WanderlustService.Facade.Users;

namespace WanderlustRest.Controllers
{
    /// <summary>
    /// A controller responsible for handling actions on users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// A facade for user operations
        /// </summary>
        private readonly IUserFacade userFacade;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userFacade">A facade for user operations</param>
        public UserController(IUserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        /// <summary>
        /// Attempts to log in a user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>True if the login was successful. Otherwise false</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            if (ModelState.IsValid)
            {
                bool result = await userFacade.AuthentizeAsync(userDto);
                return Ok(result);
            }

            var failedValidations = ExtractErrorMessagesFromModelState();
            return BadRequest(failedValidations);
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="userDto">User DTO containing information needed for a registration</param>
        /// <returns></returns>
        [HttpPost("Register")]
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
        /// Extracts error messages from model state
        /// </summary>
        /// <returns>A collection of error messages in a model state</returns>
        private IEnumerable<string> ExtractErrorMessagesFromModelState()
        {
            return ModelState.Values.SelectMany(e => e.Errors.Select(error => error.ErrorMessage));
        }
    }
}
