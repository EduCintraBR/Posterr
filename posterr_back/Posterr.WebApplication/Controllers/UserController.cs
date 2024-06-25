using Microsoft.AspNetCore.Mvc;
using Posterr.Application.Services;
using Posterr.Domain.Entities;

namespace Posterr.WebApplication.Controllers {

    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly UserService _userService;

        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [Route("v1/users")]
        public ActionResult<IEnumerable<User>> GetUsers() {

           var result = _userService.GetAll();

            return Ok(result);

        }

        [HttpGet]
        [Route("v1/users/username/{username}")]
        public ActionResult<User> GetUserByUsername(string username) {

            var result = _userService.GetFirstOrDefault(x => x.Username == username);

            if (result == null)
                return BadRequest($"'{username}' not founded.");

            return Ok(result);

        }

        [HttpGet]
        [Route("v1/users/id/{id}")]
        public ActionResult<User> GetUserById(Guid id) {

            var result = _userService.GetFirstOrDefault(x => x.Identifier == id);

            if (result == null)
                return BadRequest($"'{id}' not founded.");

            return Ok(result);

        }



    }
}
