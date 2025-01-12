using Microsoft.AspNetCore.Mvc;
using Usuario.Entity;
using Usuario.Negocio;

namespace Usuario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        
        [HttpPost(Name = "Inserir")]
        public IActionResult Inserir([FromBody] UsuarioEntity usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            usuarioNegocio.InserirUsuario(usuario);

            return Ok(new { message = "Usuário inserido com sucesso", usuario });
        }

        [HttpGet(Name = "Obter")]
        public IActionResult Obter(string cpf)
        {

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            var lista = usuarioNegocio.ListarUsuarios(cpf);

            return Ok(new { message = "Usuário obtido com sucesso", lista });
        }

        [HttpPut(Name = "Atualizar")]
        public IActionResult Atualizar([FromBody] UsuarioEntity usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.AtualizarUsuario(usuario);
            return Ok(new { message = "Usuário atualizado com sucesso", usuario });
        }

        [HttpDelete(Name = "Deletar")]
        public IActionResult Deletar(string cpf)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.DeletarUsuario(cpf);
            return Ok(new { message = "Usuário deletado com sucesso" });
        }

    }
}
