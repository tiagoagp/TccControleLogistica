using Dapper;
using NLog;
using Npgsql;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Repository
{
    public class SolicitacaoColetaRepository : ISolicitacaoColetaRepository
    {
        private Logger logger;
        private ISolicitacaoTransporteRepository solicitacaoTransporteRepository;

        public SolicitacaoColetaRepository(ISolicitacaoTransporteRepository solicitacaoTransporteRepository)
        {
            this.logger = LogManager.GetCurrentClassLogger();
            this.solicitacaoTransporteRepository = solicitacaoTransporteRepository;
        }

        public void InserirNovaSolicitacaoColeta(SolicitacaoColeta entidade)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                pgsqlConnection.Open();

                SolicitacaoTransporte solicitacaoTransporte = this.solicitacaoTransporteRepository.RetornarSolicitacaoPorId(entidade.IdSolicitacaoTransporte);

                using (var transaction = pgsqlConnection.BeginTransaction())
                {
                    try
                    {
                        pgsqlConnection.Execute("insert into solicitacaocoleta values (@id, @idsolicitacaotransporte, @registromotorista, @placaveiculo)", entidade);
                        
                        transaction.Commit();

                        this.logger.Info($"Solicitação de coleta no cliente para a solicitação de transporte nº {solicitacaoTransporte.CodigoControle} efetuada com sucesso em {DateTime.Now}.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        this.logger.Error($"Erro ao solicitar coleta no cliente para a solicitação de transporte nº {solicitacaoTransporte.CodigoControle} em {DateTime.Now}.");
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