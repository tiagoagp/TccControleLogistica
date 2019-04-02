using Dapper;
using Npgsql;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
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
            
            using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(Utilitarios.Utilitarios.RetornarStringConexao()))
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

        public int RetornarProximoCodigoControle()
        {
            try
            {
                int codigoControle = 0;

                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(Utilitarios.Utilitarios.RetornarStringConexao()))
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
    }
}