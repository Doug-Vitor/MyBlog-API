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
        private readonly HttpContextAccessorHelper _contextAccessor;

        public UsersController(IUserServices userServices, ITokenServices tokenServices, IMapper mapper, HttpContextAccessorHelper contextAccessor) => 
            (_userServices, _tokenServices, _mapper, _contextAccessor) = (userServices, tokenServices, mapper, contextAccessor);

        /// <summary>
        /// Retorna o usuário correspondente ao ID fornecido.
        /// </summary>
        /// <param name="id">O ID do usuário a ser recuperado.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400, Type = typeof(ErrorDTO))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404, Type = typeof(ErrorDTO))]
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _userServices.GetByIdAsync(id));
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="inputModel">Os dados do usuário a ser cadastrado</param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(LoginResultDTO))]
        [ProducesResponseType(400, Type = typeof(ErrorDTO))]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] CreateUserInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                int userId = await _userServices.SignUpAsync(inputModel);
                return CreatedAtAction(nameof(GetById), new { id = userId },
                    new LoginResultDTO(inputModel.Username, await _tokenServices.GenerateTokenAsync(userId)));
            }

            return DefaultInternalServerErrorResult();
        }

        /// <summary>
        /// Faz o login de um usuário.
        /// </summary>
        /// <param name="signInModel">Os dados do usuário a ser logado</param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(LoginResultDTO))]
        [ProducesResponseType(400, Type = typeof(ErrorDTO))]
        [HttpPut]
        public async Task<IActionResult> SignIn([FromBody] SignInUserModel signInModel)
        {
            if (ModelState.IsValid)
            {
                int? userId = await _userServices.SignInAsync(signInModel);
                return Ok(new LoginResultDTO(signInModel.Username, await _tokenServices
                    .GenerateTokenAsync(userId)));
            }

            return DefaultInternalServerErrorResult();
        }

        [ProducesResponseType(200)]
        [HttpPut]
        public async Task<IActionResult> SignOut()
        { 
            await _contextAccessor.SignOutUserAsync();
            return Ok();
        }

        /// <summary>
        /// Retorna os dados do usuário autenticado para uma futura atualização.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AuthenticatedUser() => Ok(await _userServices.GetAuthenticatedUserAsync());

        /// <summary>
        /// Atualiza os dados do usuário autenticado.
        /// </summary>
        /// <param name="inputModel">Os dados a serem atualizados</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorDTO))]
        [ProducesResponseType(401)]
        [Authorize]
        [HttpPatch("Update/")]
        public async Task<IActionResult> AuthenticatedUser([FromBody] CreateUserInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                await _userServices.UpdateAuthenticatedUserAsync(inputModel);
                return Ok();
            }

            return DefaultInternalServerErrorResult();
        }
    }
}