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

        /// <summary>
        /// Retorna o usuário correspondente ao ID fornecido.
        /// </summary>
        /// <param name="id">O ID do usuário a ser recuperado.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(UserViewModel))]
        [ProducesResponseType(400, Type = typeof(ErrorViewModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404, Type = typeof(ErrorViewModel))]
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
                ModelState.AddModelError(string.Empty, error.Message);
            }
            catch (NotFoundException error)
            {
                return NotFound(new ErrorViewModel(error.Message));
            }

            return BadRequest(new ErrorViewModel(ModelState.Values.SelectMany(prop => prop.Errors).Select(prop => prop.ErrorMessage).ToList()));
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="inputModel">Os dados do usuário a ser cadastrado</param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(LoginResult))]
        [ProducesResponseType(400, Type = typeof(ErrorViewModel))]
        [ProducesResponseType(500, Type = typeof(ErrorViewModel))]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] CreateUserInputModel inputModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = await _userServices.SignUpAsync(inputModel);
                    return CreatedAtAction(nameof(GetById), new { id = userId }, new LoginResult(inputModel.Username, await _tokenServices.GenerateTokenAsync(userId)));
                }
            }
            catch (ArgumentNullException error)
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }
            catch (FieldInUseException error)
            {
                return StatusCode(500, new ErrorViewModel(error.Message));
            }

            return BadRequest(new ErrorViewModel(ModelState.Values.SelectMany(prop => prop.Errors).Select(prop => prop.ErrorMessage).ToList()));
        }

        /// <summary>
        /// Faz o login de um usuário.
        /// </summary>
        /// <param name="signInModel">Os dados do usuário a ser logado</param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(LoginResult))]
        [ProducesResponseType(400, Type = typeof(ErrorViewModel))]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInUserModel signInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? userId = await _userServices.SignInAsync(signInModel);
                    return Ok(new LoginResult(signInModel.Username, await _tokenServices.GenerateTokenAsync(userId)));
                }
            }
            catch (ArgumentNullException error)
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }
            catch (SignInFailException error)
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }

            return BadRequest(new ErrorViewModel(ModelState.Values.SelectMany(prop => prop.Errors).Select(prop => prop.ErrorMessage).ToList()));
        }

        /// <summary>
        /// Atualiza os dados do usuário logado.
        /// </summary>
        /// <param name="inputModel">Os dados a serem atualizados</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorViewModel))]
        [ProducesResponseType(401)]
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] CreateUserInputModel inputModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userServices.UpdateAsync(inputModel);
                    return Ok();
                }
            }
            catch (ArgumentNullException error)
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }

            return BadRequest(new ErrorViewModel(ModelState.Values.SelectMany(prop => prop.Errors).Select(prop => prop.ErrorMessage).ToList()));
        }
    }
}