using Exame.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exame.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Usuario : ControllerBase
    {
        private IService<Domain.Usuario> _service;

        public Usuario(IService<Domain.Usuario> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get")]
        public async Task<Domain.Usuario?> GetById([FromQuery] Guid id)
        {
            var usuario = await _service.GetByIdAsync(id);

            return usuario;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IEnumerable<Domain.Usuario>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        [HttpPost]
        [Route("add")]
        public async Task Add([FromBody] Domain.Usuario usuario)
        {
            await _service.AddAsync(usuario);
        }

        [HttpPost]
        [Route("update")]
        public async Task Update([FromBody] Domain.Usuario usuario)
        {
            await _service.Update(usuario);
        }

        [HttpPost]
        [Route("update")]
        public async Task Delete([FromQuery] Guid id)
        {
            await _service.Remove(id);
        }
    }
}
