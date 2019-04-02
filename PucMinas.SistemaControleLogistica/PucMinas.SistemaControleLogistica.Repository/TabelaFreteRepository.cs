using Dapper;
using Npgsql;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Repository
{
    public class TabelaFreteRepository
    {
        public TabelaFrete RetornarTabelaFrete()
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(Utilitarios.Utilitarios.RetornarStringConexao()))
            {
                TabelaFrete tabelaFrete = pgsqlConnection.Query<TabelaFrete>("select * from tabelafrete").FirstOrDefault();
                return tabelaFrete;
            }
        }

    }
}