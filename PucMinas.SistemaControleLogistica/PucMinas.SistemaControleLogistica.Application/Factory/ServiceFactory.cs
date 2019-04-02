using PucMinas.SistemaControleLogistica.ExternalService;
using PucMinas.SistemaControleLogistica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application.Factory
{
    public class ServiceFactory
    {
        public static SolicitacaoTransporteService RetornarSolicitacaoTransporteService()
        {
            SolicitacaoTransporteRepository solicitacaoTransporteRepository = new SolicitacaoTransporteRepository();
            return new SolicitacaoTransporteService(solicitacaoTransporteRepository);
        }

        public static UsuarioService RetornarUsuarioService()
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository();
            return new UsuarioService(usuarioRepository);
        }

        public static TabelaFreteService RetornarTabelaFreteService()
        {
            ApiGoogleExternalService apiGoogleExternalService = new ApiGoogleExternalService();
            OrganizacaoRepository organizacaoRepository = new OrganizacaoRepository();
            TabelaFreteRepository tabelaFreteRepository = new TabelaFreteRepository();

            return new TabelaFreteService(apiGoogleExternalService, organizacaoRepository, tabelaFreteRepository);
        }
    }
}