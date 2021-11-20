using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterNews.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenServices;

        public UsersController(IUserServices userServices, ITokenServices tokenServices, IMapper mapper) => (_userServices, _tokenServices, _mapper) = (userServices, tokenServices, mapper);

        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int? id)
        {
            try
            {
                return Ok(await _userServices.GetByIdAsync(id));
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
            catch (NotFoundException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] CreateUserInputModel inputModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = await _userServices.SignUpAsync(inputModel);
                    return CreatedAtAction(nameof(GetById), new { id = userId }, new { user = _mapper.Map<UserViewModel>(inputModel), token = await _tokenServices.GenerateTokenAsync(userId) });
                }
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
            catch (FieldInUseException error)
            {
                return StatusCode(500, error.Message);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInUserModel signInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? userId = await _userServices.SignInAsync(signInModel);
                    return Ok(new { user = _mapper.Map<UserViewModel>(signInModel), token = await _tokenServices.GenerateTokenAsync(userId) });
                }
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
            catch (SignInFailException error)
            {
                return BadRequest(error.Message);
            }

            return BadRequest(ModelState);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CreateUserInputModel inputModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userServices.UpdateAsync(inputModel)
                    return Ok();
                }
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }

            return BadRequest(ModelState);
        }
    }
}