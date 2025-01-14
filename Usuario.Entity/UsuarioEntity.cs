using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Entity
{
    public class UsuarioEntity
    {
       
        public string Nome { get; set; }
        public int Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string? Complemento { get; set; }
        public string Sexo { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Cpf { get; set; }

    }


}

