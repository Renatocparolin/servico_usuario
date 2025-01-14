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
            string dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            string dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
            string connectionString = string.Format("Host=localhost;Database=smallrents;Username={0};Password={1}",
                dbUser, dbPassword);

            Connection = new NpgsqlConnection();
            Connection.Open();

        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
