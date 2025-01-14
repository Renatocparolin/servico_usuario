using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Entity
{
    public class RetornoEntity
    {
        public TipoRetorno CodigoErro { get; set; }


        public string Mensagem { get; set; }    

    }

    public enum TipoRetorno
    {
        Sucesso = 1,
        Erro = 2
    }   
}
