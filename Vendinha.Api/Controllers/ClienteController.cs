using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VendinhaConsole.Services;
using VendinhaConsole.Entidades;

namespace Vendinha.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService clienteService;
        private readonly IWebHostEnvironment env;

        public ClienteController(ClienteService clienteService, IWebHostEnvironment env)
        {
            this.clienteService = clienteService;
            this.env = env;
        }
        [HttpGet]
        public IActionResult Listar(string pesquisa, int page = 0, int pageSize = 0)
        {
            // ternário
            var clientes = string.IsNullOrEmpty(pesquisa) ?
                clienteService.Listar(page, pageSize) :
                clienteService.Listar(page, pageSize, pesquisa);
            return Ok(clientes);
        }

        [HttpGet("{codigo}")]
        public IActionResult GetByCodigo(int codigo)
        {
            var cliente = clienteService.Retorna(codigo);
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest(ModelState);
            }

            var sucesso = clienteService.Criar(cliente,
                out List<ValidationResult> erros);
            if (sucesso)
            {
                return Ok(cliente);
            }
            else
            {
                return UnprocessableEntity(erros);
            }
        }

        [HttpPut]
        public IActionResult Editar([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest(ModelState);
            }

            var sucesso = clienteService.Editar(cliente,
                out List<ValidationResult> erros);
            if (sucesso)
            {
                return Ok(cliente);
            }
            else if (erros.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return UnprocessableEntity(erros);
            }
        }
        /*
         CRUD - Create/Recover/Update/Delete
         */
        // DELETE /api/cliente/{codigo}
        [HttpDelete("{codigo}")] // concatena com o Route
        public IActionResult Remover(int codigo)
        {
            var cliente = clienteService.Excluir(codigo, out _);
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cliente);
            }
        }
    }
}
