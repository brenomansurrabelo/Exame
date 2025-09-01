using Exame.Services;
using Exame.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exame.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            
            return Ok(usuarios); 
        }

        [HttpGet]
        [Route("find")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> Get([FromQuery] string? nome, [FromQuery] string? endereco)
        {
            var usuarios = await _usuarioService.Find(nome, endereco);

            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioDTO dto) 
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                await _usuarioService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar usuário: {ex.Message}"); 
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Update(Guid id, [FromBody] UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.Id = id;

            try
            {
                await _usuarioService.UpdateAsync(dto);
                
                return Ok(); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar usuário: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) 
        {
            try
            {
                await _usuarioService.DeleteAsync(id);
                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir usuário: {ex.Message}");
            }
        }
    }
}