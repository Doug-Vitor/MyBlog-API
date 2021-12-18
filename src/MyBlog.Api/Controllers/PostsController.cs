using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Api.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostServices _postServices;

        public PostsController(IPostServices postServices) => _postServices = postServices;

        /// <summary>
        /// Cria uma nova publicação.
        /// </summary>
        /// <param name="createdPost">A publicação a ser criada.</param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(PostDTO))]
        [ProducesResponseType(500, Type = typeof(ErrorDTO))]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreatePostInputModel createdPost)
        {
            try
            {
                int postId = await _postServices.InsertAsync(createdPost);
                return CreatedAtAction(nameof(GetById), new { id = postId }, await _postServices.GetByIdAsync(postId));
            }
            catch (Exception error) when (error is ArgumentException || error is ArgumentNullException)
            {
                return BadRequest(new ErrorDTO(error.Message));
            }
        }

        /// <summary>
        /// Retorna uma publicação correspondente ao ID fornecido.
        /// </summary>
        /// <param name="id">O ID da publicação a ser retornada.</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(PostDTO))]
        [ProducesResponseType(404, Type = typeof(ErrorDTO))]
        [ProducesResponseType(500, Type = typeof(ErrorDTO))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int? id)
        {
            try
            {
                return Ok(await _postServices.GetByIdAsync(id));
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(new ErrorDTO(error.Message));
            }
            catch (NotFoundException error)
            {
                return NotFound(new ErrorDTO(error.Message));
            }
        }

        /// <summary>
        /// Retorna todas as publicações.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<PostDTO>))]
        [HttpGet]
        [ProducesResponseType(500, Type = typeof(ErrorDTO))]
        public async Task<IActionResult> GetAll() => Ok(await _postServices.GetAllAsync());

        /// <summary>
        /// Atualiza a publicação correspondente ao ID fornecido.
        /// </summary>
        /// <param name="id">O ID da publicação a ser excluída.</param>
        /// <param name="updatedPost">A publicação com dados atualizados</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401, Type = typeof(ErrorDTO))]
        [ProducesResponseType(500, Type = typeof(ErrorDTO))]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int? id, [FromBody] CreatePostInputModel updatedPost)
        {
            try
            {
                await _postServices.UpdateAsync(id, updatedPost);
                return Ok();
            }
            catch (Exception error) when (error is ArgumentException || error is ArgumentNullException)
            {
                return BadRequest(new ErrorDTO(error.Message));
            }
            catch (UnauthorizedAccessException error)
            {
                return Unauthorized(new ErrorDTO(error.Message));
            }
        }

        /// <summary>
        /// Exclui a publicação correspondente ao ID fornecido.
        /// </summary>
        /// <param name="id">O ID da publicação a ser excluída.</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401, Type = typeof(ErrorDTO))]
        [ProducesResponseType(500, Type = typeof(ErrorDTO))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] int? id)
        {
            try
            {
                await _postServices.RemoveAsync(id);
                return Ok();
            }
            catch (ArgumentNullException error)
            {
                return BadRequest(new ErrorDTO(error.Message));
            }
            catch (UnauthorizedAccessException error)
            {
                return Unauthorized(new ErrorDTO(error.Message));
            }
        }
    }
}
