using Microsoft.AspNetCore.Mvc;
using Usuario.Entity;
using Usuario.Negocio;

namespace Usuario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        
        [HttpPost("Inserir")]
        public IActionResult Inserir([FromBody] UsuarioEntity usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            var retorno = usuarioNegocio.InserirUsuario(usuario);

            return Ok(new { message = string.Concat(retorno.CodigoErro,"-",retorno.Mensagem), usuario });
        }

        [HttpGet("Obter")]
        public IActionResult Obter(string cpf)
        {

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            var lista = usuarioNegocio.ListarUsuarios(cpf);

            return Ok(new { message = "Usuário obtido com sucesso", lista });
        }

        [HttpPut("Atualizar")]
        public IActionResult Atualizar([FromBody] UsuarioEntity usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            
            var retorno = usuarioNegocio.AtualizarUsuario(usuario);

            return Ok(new { message = string.Concat(retorno.CodigoErro, "-", retorno.Mensagem), usuario });
        }

        [HttpDelete("Deletar")]
        public IActionResult Deletar(string cpf)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.DeletarUsuario(cpf);
            return Ok(new { message = "Usuário deletado com sucesso" });
        }

    }
}
