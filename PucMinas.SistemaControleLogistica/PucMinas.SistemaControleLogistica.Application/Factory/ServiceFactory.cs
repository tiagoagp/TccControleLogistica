using PucMinas.SistemaControleLogistica.Application.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
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
        public static ISolicitacaoTransporteService RetornarSolicitacaoTransporteService()
        {
            ISolicitacaoTransporteRepository solicitacaoTransporteRepository = new SolicitacaoTransporteRepository();
            ITabelaFreteService tabelaFreteService = RetornarTabelaFreteService();
            return new SolicitacaoTransporteService(solicitacaoTransporteRepository, tabelaFreteService);
        }

        public static IUsuarioService RetornarUsuarioService()
        {
            IUsuarioRepository usuarioRepository = new UsuarioRepository();
            return new UsuarioService(usuarioRepository);
        }

        public static ISolicitacaoColetaService RetornarSolicitacaoColetaService()
        {
            ISolicitacaoTransporteRepository solicitacaoTransporteRepository = new SolicitacaoTransporteRepository();
            ISolicitacaoColetaRepository solicitacaoColetaRepository = new SolicitacaoColetaRepository(solicitacaoTransporteRepository);
            return new SolicitacaoColetaService(solicitacaoColetaRepository, solicitacaoTransporteRepository);
        }

        public static ITabelaFreteService RetornarTabelaFreteService()
        {
            IApiGoogleExternalService apiGoogleExternalService = new ApiGoogleExternalService();
            IOrganizacaoRepository organizacaoRepository = new OrganizacaoRepository();
            ITabelaFreteRepository tabelaFreteRepository = new TabelaFreteRepository();

            return new TabelaFreteService(apiGoogleExternalService, organizacaoRepository, tabelaFreteRepository);
        }

        public static ISistemaGestaoFrotaService RetornarSistemaGestaoFrotaService()
        {
            ISistemaGestaoFrotaExternalService sistemaGestaoFrotaExternalService = new SistemaGestaoFrotaExternalService();
            return new SistemaGestaoFrotaService(sistemaGestaoFrotaExternalService);
        }
    }
}