using Usuario.Entity;
using Usuario.Dados;
using Microsoft.Extensions.Configuration;

namespace Usuario.Negocio
{
    public class UsuarioNegocio
    {

        public bool InserirUsuario(UsuarioEntity usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            if (usuario.Id == 0)
            {
                throw new ArgumentException("Id é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                throw new ArgumentException("Nome é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(usuario.Cpf))
            {
                throw new ArgumentException("CPF é obrigatório.");
            }

            if (!ValidarCPF(usuario.Cpf))
            {
                throw new ArgumentException("CPF inválido.");
            }

            if (string.IsNullOrEmpty(usuario.Sexo))
            {
                throw new ArgumentException("Sexo é obrigatório.");
            }

            if (string.IsNullOrEmpty(usuario.Data_Nascimento.ToString()))
            {
                throw new ArgumentException("Data de nascimento é obrigatória.");
            }

            if (!ValidarData(usuario.Data_Nascimento.ToString()))
            {
                throw new ArgumentException("Data de nascimento inválida.");
            }

            if (usuario.Cep == 0)
            {
                throw new ArgumentException("CEP é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Logradouro))
            {
                throw new ArgumentException("Logradouro é obrigatório.");
            }

            if (usuario.Numero == 0)
            {
                throw new ArgumentException("Número é obrigatório.");
            }

            UsuariosDados usuariosDados = new UsuariosDados();
            return usuariosDados.InserirUsuario(usuario);
        }


        public List<UsuarioEntity> ListarUsuarios(string cpf)
        {
            UsuariosDados usuariosDados = new UsuariosDados();
            return usuariosDados.ListarUsuarios(cpf);
        }

        public bool AtualizarUsuario(UsuarioEntity usuario)
        {
            UsuariosDados usuariosDados = new UsuariosDados();
            return usuariosDados.AtualizarUsuario(usuario);
        }

        public bool DeletarUsuario(string cpf)
        {
            UsuariosDados usuariosDados = new UsuariosDados();
            return usuariosDados.DeletarUsuario(cpf);
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

    }
}

