using Dapper;
using Npgsql;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Repository
{
    public class SolicitacaoColetaRepository
    {
        public void InserirNovaSolicitacaoColeta(SolicitacaoColeta entidade)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                pgsqlConnection.Open();

                using (var transaction = pgsqlConnection.BeginTransaction())
                {
                    try
                    {
                        pgsqlConnection.Execute("insert into solicitacaocoleta values (@idsolicitacaotransporte, @registromotorista, @placaveiculo)", entidade);
                        
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }

        public List<SolicitacaoColeta> RetornarSolicitacoesColeta()
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                List<SolicitacaoColeta> lista = pgsqlConnection.Query<SolicitacaoColeta>("SELECT * FROM solicitacaocoleta").AsList();

                foreach (SolicitacaoColeta solic in lista)
                {
                    SolicitacaoTransporte entidade = pgsqlConnection.Query<SolicitacaoTransporte>("SELECT * FROM solicitacaotransporte where id = @ID", new { Id = solic.IdSolicitacaoTransporte }).FirstOrDefault();

                    if (entidade != null)
                    {
                        solic.CodigoControleSolicitacao = entidade.CodigoControle;
                        solic.StatusSolicitacao = entidade.Status;
                    }
                }

                return lista;
            }
        }
    }
}