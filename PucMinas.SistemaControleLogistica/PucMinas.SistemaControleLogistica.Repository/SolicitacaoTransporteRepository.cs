using Dapper;
using NLog;
using Npgsql;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using PucMinas.SistemaControleLogistica.Domain.Filtros;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Repository
{
    public class SolicitacaoTransporteRepository : ISolicitacaoTransporteRepository
    {
        private Logger logger;

        public SolicitacaoTransporteRepository()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void InserirNovaSolicitacao(SolicitacaoTransporte entidade)
        {   
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                pgsqlConnection.Open();

                using (var transaction = pgsqlConnection.BeginTransaction())
                {
                    try
                    {
                        pgsqlConnection.Execute("insert into solicitacaotransporte values (@id, @nomerecebedor, @ruadestino, @telefonerecebedor, @numerodestino, @complementodestino, @cepdestino, @bairrodestino, @cidadedestino, @estadodestino, @pontoreferenciaentrega, @numeronf, @serienf, @chaveacessonf, @dataentrega, @dataemissaonf, @valorfrete, @status, @usuarioid, @emailrecebedor, @codigocontrole )", entidade);

                        foreach (Produto prod in entidade.Produtos)
                        {
                            pgsqlConnection.Execute("insert into solicitacaoproduto values (@id, @quantidade, @peso, @altura, @largura, @comprimento, @solicitacaoid, @descricaoproduto)", prod);
                        }

                        transaction.Commit();

                        this.logger.Info($"Solicitação de transporte nº {entidade.CodigoControle} aberta com sucesso em {DateTime.Now}.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        this.logger.Error($"Erro ao tentar abrir a solicitação de transporte nº {entidade.CodigoControle} em {DateTime.Now}.");
                        throw e;
                    }
                }
            }
        }

        public void EditarSolicitacao(SolicitacaoTransporte entidade)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                pgsqlConnection.Open();

                using (var transaction = pgsqlConnection.BeginTransaction())
                {
                    try
                    {
                        pgsqlConnection.Execute(" update solicitacaotransporte " +
                                                " set nomerecebedor = @nomerecebedor, ruadestino = @ruadestino, telefonerecebedor = @telefonerecebedor, numerodestino = @numerodestino, complementodestino = @complementodestino, cepdestino = @cepdestino, bairrodestino = @bairrodestino, cidadedestino = @cidadedestino, estadodestino = @estadodestino, pontoreferenciaentrega = @pontoreferenciaentrega, numeronf = @numeronf, serienf = @serienf, chaveacessonf = @chaveacessonf, dataentrega = @dataentrega, dataemissaonf = @dataemissaonf, valorfrete = @valorfrete, status = @status, usuarioid = @usuarioid, emailrecebedor = @emailrecebedor, codigocontrole = @codigocontrole " +
                                                " where id = @id", entidade);

                        pgsqlConnection.Execute("delete from solicitacaoproduto where solicitacaoid = @solicitacaoid", new { solicitacaoid = entidade .Id });

                        foreach (Produto prod in entidade.Produtos)
                        {
                            pgsqlConnection.Execute("insert into solicitacaoproduto values (@id, @quantidade, @peso, @altura, @largura, @comprimento, @solicitacaoid, @descricaoproduto)", prod);
                        }

                        transaction.Commit();

                        this.logger.Info($"Solicitação de transporte nº {entidade.CodigoControle} alterada com sucesso em {DateTime.Now}.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        this.logger.Error($"Erro ao tentar alterar a solicitação de transporte nº {entidade.CodigoControle} em {DateTime.Now}.");
                        throw e;
                    }
                }
            }
        }

        public void AlterarStatusSolicitacaoSolicitacao(SolicitacaoTransporte entidade)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                pgsqlConnection.Open();

                using (var transaction = pgsqlConnection.BeginTransaction())
                {
                    try
                    {
                        pgsqlConnection.Execute(" update solicitacaotransporte " +
                                                " set status = @status " +
                                                " where id = @id", entidade);
                        
                        transaction.Commit();

                        this.logger.Info($"Alteração de status da solicitação de transporte nº {entidade.CodigoControle} efetuada com sucesso em {DateTime.Now}.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        this.logger.Error($"Erro ao tentar alterar o status da solicitação de transporte nº {entidade.CodigoControle} em {DateTime.Now}.");
                        throw e;
                    }
                }
            }
        }

        public int RetornarProximoCodigoControle()
        {
            try
            {
                int codigoControle = 0;

                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
                {
                    pgsqlConnection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT codigocontrole FROM solicitacaotransporte order by CodigoControle desc limit 1", pgsqlConnection))
                    {
                        NpgsqlDataReader rd = cmd.ExecuteReader();

                        while (rd.Read())
                        {
                            int.TryParse(rd["codigocontrole"].ToString(), out codigoControle);
                        }
                    }
                }

                return codigoControle + 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SolicitacaoTransporte RetornarSolicitacaoPorId(Guid id)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                SolicitacaoTransporte entidade = pgsqlConnection.Query<SolicitacaoTransporte>("SELECT * FROM solicitacaotransporte where id = @ID", new { Id = id }).FirstOrDefault();
                
                if (entidade != null)
                {
                    List<Produto> produtos = pgsqlConnection.Query<Produto>("SELECT * FROM solicitacaoproduto where solicitacaoid = @solicitacaoid", new { solicitacaoid = entidade.Id }).AsList();
                    entidade.Produtos = produtos;
                }
                
                return entidade;
            }
        }

        public List<SolicitacaoTransporte> RetornarSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                string queryTexto = "";
                var dynamicParameters = new DynamicParameters();

                if (filtrosConsulta.IdCliente != Guid.Empty)
                {
                    queryTexto = " where usuarioid = @usuarioid";
                    dynamicParameters.Add("usuarioid", filtrosConsulta.IdCliente);
                }

                if (filtrosConsulta.DataInicial != DateTime.MinValue)
                {
                    if (string.IsNullOrEmpty(queryTexto))
                    {
                        queryTexto += " where ";
                    }
                    else
                    {
                        queryTexto += " and ";
                    }

                    queryTexto = " where dataentrega >= @dataentrega1";
                    dynamicParameters.Add("dataentrega1", filtrosConsulta.DataInicial);
                }

                if (filtrosConsulta.DataFinal != DateTime.MinValue)
                {
                    if (string.IsNullOrEmpty(queryTexto))
                    {
                        queryTexto += " where ";
                    }
                    else
                    {
                        queryTexto += " and ";
                    }

                    queryTexto += " dataentrega <= @dataentrega2";
                    dynamicParameters.Add("dataentrega2", filtrosConsulta.DataFinal);
                }

                if(filtrosConsulta.Status != StatusSolicitacao.Todos)
                {
                    if (string.IsNullOrEmpty(queryTexto))
                    {
                        queryTexto += " where ";
                    }
                    else
                    {
                        queryTexto += " and ";
                    }

                    queryTexto += " status = @status";
                    dynamicParameters.Add("status", (int)filtrosConsulta.Status);
                }

                dynamicParameters.Add("Limit", filtrosConsulta.Limit);
                dynamicParameters.Add("Offset", filtrosConsulta.Offset);

                List<SolicitacaoTransporte> lista = pgsqlConnection.Query<SolicitacaoTransporte>("SELECT * FROM solicitacaotransporte" + queryTexto + " ORDER BY codigocontrole LIMIT @Limit OFFSET @Offset", dynamicParameters).AsList();

                foreach (SolicitacaoTransporte solic in lista)
                {
                    solic.Usuario = pgsqlConnection.Query<Usuario>("SELECT * FROM usuario WHERE Id = @Id", new { Id = solic.UsuarioId }).FirstOrDefault();
                }

                return lista;
            }
        }

        public int RetornarTotalSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                string queryTexto = "";
                var dynamicParameters = new DynamicParameters();

                if (filtrosConsulta.IdCliente != Guid.Empty)
                {
                    queryTexto = " where usuarioid = @usuarioid";
                    dynamicParameters.Add("usuarioid", filtrosConsulta.IdCliente);
                }

                if (filtrosConsulta.DataInicial != DateTime.MinValue)
                {
                    if (string.IsNullOrEmpty(queryTexto))
                    {
                        queryTexto += " where ";
                    }
                    else
                    {
                        queryTexto += " and ";
                    }

                    queryTexto = " where dataentrega >= @dataentrega1";
                    dynamicParameters.Add("dataentrega1", filtrosConsulta.DataInicial);
                }

                if (filtrosConsulta.DataFinal != DateTime.MinValue)
                {
                    if (string.IsNullOrEmpty(queryTexto))
                    {
                        queryTexto += " where ";
                    }
                    else
                    {
                        queryTexto += " and ";
                    }

                    queryTexto += " dataentrega <= @dataentrega2";
                    dynamicParameters.Add("dataentrega2", filtrosConsulta.DataFinal);
                }

                if (filtrosConsulta.Status != StatusSolicitacao.Todos)
                {
                    if (string.IsNullOrEmpty(queryTexto))
                    {
                        queryTexto += " where ";
                    }
                    else
                    {
                        queryTexto += " and ";
                    }

                    queryTexto += " status = @status";
                    dynamicParameters.Add("status", (int)filtrosConsulta.Status);
                }

                int quantidade = pgsqlConnection.ExecuteScalar<int>("SELECT Count(1) FROM solicitacaotransporte" + queryTexto, dynamicParameters);
                
                return quantidade;
            }
        }
    }
}