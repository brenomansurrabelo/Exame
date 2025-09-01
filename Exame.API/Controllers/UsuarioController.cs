using Exame.Services;
using Exame.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Exame.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(Guid id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado");

            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioDTO>> GetAll()
        {
            return await _usuarioService.GetAllAsync();
        }

        [HttpPost]
        public async Task Create([FromBody] UsuarioDTO dto)
        {
            await _usuarioService.CreateAsync(dto);
        }

        [HttpPut]
        public async Task Update([FromBody] UsuarioDTO dto)
        {
            await _usuarioService.UpdateAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _usuarioService.DeleteAsync(id);
        }
    }
}