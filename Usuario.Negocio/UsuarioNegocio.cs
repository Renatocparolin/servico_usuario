using Usuario.Entity;
using Usuario.Dados;
using Microsoft.Extensions.Configuration;
using System.Xml;

namespace Usuario.Negocio
{
    public class UsuarioNegocio
    {

        public RetornoEntity InserirUsuario(UsuarioEntity usuario)
        {

            var validaUsuario = ValidaDados(usuario);

            if (validaUsuario.CodigoErro == TipoRetorno.Sucesso)
            {
                if (!UsuarioExiste(usuario.Cpf))
                {
                    UsuariosDados usuariosDados = new UsuariosDados();
                    var inseriuUsuario = usuariosDados.InserirUsuario(usuario);

                    if (inseriuUsuario)
                    {
                        var retorno = new RetornoEntity();
                        retorno.Mensagem = "Usuário inserido com sucesso!";
                        retorno.CodigoErro = TipoRetorno.Sucesso;

                        return retorno;
                    }
                    else
                    {
                        var retorno = new RetornoEntity();
                        retorno.Mensagem = "Erro ao inserir usuário";
                        retorno.CodigoErro = TipoRetorno.Erro;
                        return retorno;
                    }


                }
                else
                {
                    var retorno = new RetornoEntity();
                    retorno.Mensagem = "Usuário já existe na base de dados";
                    retorno.CodigoErro = TipoRetorno.Erro;
                    return retorno;

                }
            }
            else
            {
                return validaUsuario;
            }
        }

        public bool UsuarioExiste(string cpf)
        {
            UsuariosDados usuariosDados = new UsuariosDados();
            var retorno = usuariosDados.ListarUsuarios(cpf);

            if (retorno.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<UsuarioEntity> ListarUsuarios(string cpf)
        {
            UsuariosDados usuariosDados = new UsuariosDados();
            return usuariosDados.ListarUsuarios(cpf);
        }

        public RetornoEntity AtualizarUsuario(UsuarioEntity usuario)
        {
           var validaUsuario = ValidaDados(usuario);

            if (validaUsuario.CodigoErro == TipoRetorno.Sucesso)
            {
                if (!UsuarioExiste(usuario.Cpf))
                {
                    var retorno = new RetornoEntity();
                    retorno.Mensagem = "Usuário não existe na base de dados";
                    retorno.CodigoErro = TipoRetorno.Erro;
                    return retorno;
                }
                else
                {
                    UsuariosDados usuariosDados = new UsuariosDados();

                    var atualizouUsuario = usuariosDados.AtualizarUsuario(usuario);

                    if (atualizouUsuario)
                    {
                        var retorno = new RetornoEntity();
                        retorno.Mensagem = "Usuário atualizado com sucesso!";
                        retorno.CodigoErro = TipoRetorno.Sucesso;
                        return retorno;
                    }
                    else
                    {
                        var retorno = new RetornoEntity();
                        retorno.Mensagem = "Erro ao atualizar usuário";
                        retorno.CodigoErro = TipoRetorno.Erro;
                        return retorno;
                    }
                }
            }
            else
            {
                return validaUsuario;
            }
        }

        public RetornoEntity DeletarUsuario(string cpf)
        {

            if (!UsuarioExiste(cpf))
            {
                var retorno = new RetornoEntity();
                retorno.Mensagem = "Usuário não existe na base de dados";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            UsuariosDados usuariosDados = new UsuariosDados();
            var deletouUsuario = usuariosDados.DeletarUsuario(cpf);

            if (deletouUsuario)
            {
                var retorno = new RetornoEntity();
                retorno.Mensagem = "Usuário deletado com sucesso!";
                retorno.CodigoErro = TipoRetorno.Sucesso;
                return retorno;
            }
            else
            {
                var retorno = new RetornoEntity();
                retorno.Mensagem = "Erro ao deletar usuário";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }   
        }



        public static bool ValidarCPF(string cpf)
        {

           
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11) return false;

            // Verifica se todos os dígitos são iguais (ex: 111.111.111-11)
            if (cpf.Distinct().Count() == 1) return false;

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            // Calcula o primeiro dígito verificador
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }


        public static bool ValidarData(string data)
        {
            return DateTime.TryParse(data, out _);

        }

        public RetornoEntity ValidaDados(UsuarioEntity usuario)
        {
            
            var retorno = new RetornoEntity();
            retorno.CodigoErro = TipoRetorno.Sucesso;
            retorno.Mensagem = "Dados válidos";

            if (usuario == null)
            {
                retorno.Mensagem = "Usuário é obrigatório";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                retorno.Mensagem = "Nome é obrigatório.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (string.IsNullOrWhiteSpace(usuario.Cpf))
            {
                retorno.Mensagem = "CPF é obrigatório.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (!ValidarCPF(usuario.Cpf))
            {
               retorno.Mensagem = "CPF inválido.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (string.IsNullOrEmpty(usuario.Sexo))
            {
                retorno.Mensagem = "Sexo é obrigatório.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (string.IsNullOrEmpty(usuario.Data_Nascimento.ToString()))
            {
                retorno.Mensagem = "Data de nascimento é obrigatória.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (!ValidarData(usuario.Data_Nascimento.ToString()))
            {
                retorno.Mensagem = "Data de nascimento inválida.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (usuario.Cep == 0)
            {
                retorno.Mensagem = "CEP é obrigatório.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (string.IsNullOrWhiteSpace(usuario.Logradouro))
            {
                retorno.Mensagem = "Logradouro é obrigatório.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            if (usuario.Numero == 0)
            {
                retorno.Mensagem = "Número é obrigatório.";
                retorno.CodigoErro = TipoRetorno.Erro;
                return retorno;
            }

            return retorno;
           
        }

    }
}

