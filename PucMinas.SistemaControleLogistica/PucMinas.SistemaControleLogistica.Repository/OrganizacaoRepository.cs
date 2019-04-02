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
    public class OrganizacaoRepository
    {
        public Organizacao RetornarOrganizacao()
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(Utilitarios.Utilitarios.RetornarStringConexao()))
            {
                Organizacao organizacao = pgsqlConnection.Query<Organizacao>("select * from organizacao").FirstOrDefault();
                return organizacao;
            }
        }
    }
}