using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Usuario.Dados;
using Usuario.Entity;
using Usuario.Negocio;
using System.Collections.Generic;

namespace Usuario.Tests
{
    [TestClass]
    public class UsuarioNegocioTests
    {
         private UsuarioNegocio _usuarioNegocio;

       

        [TestMethod]
        public void InserirUsuario_ValidUser_ReturnsSuccess()
        {
            var usuario = new UsuarioEntity { Nome = "John Doe", Cpf = "12345678909", Sexo = "M", Data_Nascimento = new DateTime(1990, 1, 1), Cep = 12345, Logradouro = "Street", Numero = 1 };
            
            _usuarioNegocio = new UsuarioNegocio();
            var result = _usuarioNegocio.InserirUsuario(usuario);

            Assert.AreEqual(TipoRetorno.Sucesso, result.CodigoErro);
            Assert.AreEqual("Usuário inserido com sucesso!", result.Mensagem);
        }

        [TestMethod]
        public void InserirUsuario_UserExists_ReturnsError()
        {
            var usuario = new UsuarioEntity { Nome = "John Doe", Cpf = "13187574017", Sexo = "M", Data_Nascimento = new DateTime(1990, 1, 1), Cep = 12345, Logradouro = "Street", Numero = 1 };

            _usuarioNegocio = new UsuarioNegocio();

            var result = _usuarioNegocio.InserirUsuario(usuario);

            Assert.AreEqual(TipoRetorno.Erro, result.CodigoErro);
            Assert.AreEqual("Usuário já existe na base de dados", result.Mensagem);
        }

        [TestMethod]
        public void AtualizarUsuario_ValidUser_ReturnsSuccess()
        {
            var usuario = new UsuarioEntity { Nome = "John Doe", Cpf = "13187574017", Sexo = "M", Data_Nascimento = new DateTime(1990, 1, 1), Cep = 12345, Logradouro = "Street", Numero = 1 };
            _usuarioNegocio = new UsuarioNegocio();
            var result = _usuarioNegocio.AtualizarUsuario(usuario);

            Assert.AreEqual(TipoRetorno.Sucesso, result.CodigoErro);
            Assert.AreEqual("Usuário atualizado com sucesso!", result.Mensagem);
        }

        [TestMethod]
        public void AtualizarUsuario_UserDoesNotExist_ReturnsError()
        {
            var usuario = new UsuarioEntity { Nome = "John Doe", Cpf = "13187574017", Sexo = "M", Data_Nascimento = new DateTime(1990, 1, 1), Cep = 12345, Logradouro = "Street", Numero = 1 };

            _usuarioNegocio = new UsuarioNegocio();
            var result = _usuarioNegocio.AtualizarUsuario(usuario);

            Assert.AreEqual(TipoRetorno.Erro, result.CodigoErro);
            Assert.AreEqual("Usuário não existe na base de dados", result.Mensagem);
        }

        [TestMethod]
        public void DeletarUsuario_UserExists_ReturnsSuccess()
        {
            var cpf = "13187574017";
            _usuarioNegocio = new UsuarioNegocio();
            var result = _usuarioNegocio.DeletarUsuario(cpf);

            Assert.AreEqual(TipoRetorno.Sucesso, result.CodigoErro);
            Assert.AreEqual("Usuário deletado com sucesso!", result.Mensagem);
        }

        [TestMethod]
        public void DeletarUsuario_UserDoesNotExist_ReturnsError()
        {
            var cpf = "13187574017";
            _usuarioNegocio = new UsuarioNegocio();
            var result = _usuarioNegocio.DeletarUsuario(cpf);

            Assert.AreEqual(TipoRetorno.Erro, result.CodigoErro);
            Assert.AreEqual("Usuário não existe na base de dados", result.Mensagem);
        }

        [TestMethod]
        public void ValidarCPF_ValidCPF_ReturnsTrue()
        {
            var cpf = "13187574017";
            var result = UsuarioNegocio.ValidarCPF(cpf);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidarCPF_InvalidCPF_ReturnsFalse()
        {
            var cpf = "13187574017";
            var result = UsuarioNegocio.ValidarCPF(cpf);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidarData_ValidDate_ReturnsTrue()
        {
            var data = "1990-01-01";
            var result = UsuarioNegocio.ValidarData(data);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidarData_InvalidDate_ReturnsFalse()
        {
            var data = "invalid-date";
            var result = UsuarioNegocio.ValidarData(data);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidaDados_ValidUser_ReturnsSuccess()
        {
            var usuario = new UsuarioEntity { Nome = "John Doe", Cpf = "13187574017", Sexo = "M", Data_Nascimento = new DateTime(1990, 1, 1), Cep = 12345, Logradouro = "Street", Numero = 1 };
            _usuarioNegocio = new UsuarioNegocio();
            var result = _usuarioNegocio.ValidaDados(usuario);

            Assert.AreEqual(TipoRetorno.Sucesso, result.CodigoErro);
            Assert.AreEqual("Dados válidos", result.Mensagem);
        }

        [TestMethod]
        public void ValidaDados_InvalidUser_ReturnsError()
        {
            _usuarioNegocio = new UsuarioNegocio();
            var usuario = new UsuarioEntity { Nome = "John Doe", Cpf = "13187574017", Sexo = "M", Data_Nascimento = new DateTime(1990, 1, 1), Cep = 12345, Logradouro = "Street", Numero = 1 };
            var result = _usuarioNegocio.ValidaDados(usuario);

            Assert.AreEqual(TipoRetorno.Erro, result.CodigoErro);
            Assert.AreNotEqual("Dados válidos", result.Mensagem);
        }
    }
}
