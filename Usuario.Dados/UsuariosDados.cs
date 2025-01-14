using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Usuario.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Dapper;

namespace Usuario.Dados
{
    public class UsuariosDados
    {


        public bool InserirUsuario(UsuarioEntity usuario)
        {
            try
            {
                using var conn = new conexaoBanco();

                string query = @"insert into usuario (nome, cep, logradouro, numero, complemento, sexo, data_nascimento, cpf) values (@nome, @cep, @logradouro, @numero, @complemento, @sexo, @data_nascimento, @cpf);";


                var result = conn.Connection.Execute(sql: query, param: usuario);
                return result == 1;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public List<UsuarioEntity> ListarUsuarios(string cpf)
        {
            try
            {
                using var conn = new conexaoBanco();
                string query = @"select * from usuario where cpf = @cpf;";
                var result = conn.Connection.Query<UsuarioEntity>(sql: query, param: new { cpf });
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AtualizarUsuario(UsuarioEntity usuario)
        {
            try
            {
                using var conn = new conexaoBanco();
                string query = @"update usuario set nome = @nome, cep = @cep, logradouro = @logradouro, numero = @numero, complemento = @complemento, sexo = @sexo, data_nascimento = @data_nascimento where cpf = @cpf;";
                var result = conn.Connection.Execute(sql: query, param: usuario);
                return result == 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeletarUsuario(string cpf)
        {
            try
            {
                using var conn = new conexaoBanco();
                string query = @"delete from usuario where cpf = @cpf;";
                var result = conn.Connection.Execute(sql: query, param: new { cpf });
                return result == 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
