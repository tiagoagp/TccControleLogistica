using Dapper;
using Npgsql;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Repository
{
    public class SolicitacaoTransporteRepository
    {
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
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
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
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
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
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
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

        public List<SolicitacaoTransporte> RetornarSolicitacoes(DateTime dataInicial, DateTime dataFinal, Guid idCliente)
        {
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(DadosAutenticacao.RetornarStringConexao()))
            {
                string queryTexto = "";
                var dynamicParameters = new DynamicParameters();

                if (idCliente != Guid.Empty)
                {
                    queryTexto = " where usuarioid = @usuarioid";
                    dynamicParameters.Add("usuarioid", idCliente);
                }

                if (dataInicial != DateTime.MinValue)
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
                    dynamicParameters.Add("dataentrega1", dataInicial);
                }

                if (dataFinal != DateTime.MinValue)
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
                    dynamicParameters.Add("dataentrega2", dataFinal);
                }

                List<SolicitacaoTransporte> lista = pgsqlConnection.Query<SolicitacaoTransporte>("SELECT * FROM solicitacaotransporte" + queryTexto, dynamicParameters).AsList();

                foreach (SolicitacaoTransporte solic in lista)
                {
                    solic.Usuario = pgsqlConnection.Query<Usuario>("SELECT * FROM usuario WHERE Id = @Id", new { Id = solic.UsuarioId }).FirstOrDefault();
                }

                return lista;
            }
        }
    }
}