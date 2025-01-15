using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Usuario.Dados
{
    public class conexaoBanco : IDisposable
    {
        
        public NpgsqlConnection Connection { get; set; }

        public conexaoBanco()
        {
            Connection = new NpgsqlConnection("Host=192.168.10.5;Database=smallrents;Username=postgres;Password=photosmarT@01");
            Connection.Open();

        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
