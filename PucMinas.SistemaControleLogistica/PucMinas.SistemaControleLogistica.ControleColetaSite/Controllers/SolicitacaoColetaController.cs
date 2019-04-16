using Newtonsoft.Json;
using PucMinas.SistemaControleLogistica.ControleColetaSite.API;
using PucMinas.SistemaControleLogistica.ControleColetaSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.Controllers
{
    [Authorize(Roles = "Transportadora")]
    public class SolicitacaoColetaController : Controller
    {
        public ActionResult Coletas()
        {
            DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];
            var token = dados.Token;

            var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = client.GetAsync($"{UrlApi.RetornarUrlWebApi()}api/SolicitacaoColeta", new CancellationToken()).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            API.Request.CheckRequest(response.StatusCode, responseString);

            List<SolicitacaoColetaModel> lista = JsonConvert.DeserializeObject<List<SolicitacaoColetaModel>>(responseString);

            return View(lista);
        }

        // GET: SolicitacaoColeta
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RetornarVeiculosDisponiveis()
        {
            try
            {
                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync($"{UrlApi.RetornarUrlWebApi()}api/GestaoFrotasVeiculo", new CancellationToken()).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                List<Veiculo> lista = JsonConvert.DeserializeObject<List<Veiculo>>(responseString);

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult RetornarMotoristasDisponiveis()
        {
            try
            {
                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync($"{UrlApi.RetornarUrlWebApi()}api/GestaoFrotasMotorista", new CancellationToken()).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                List<Motorista> lista = JsonConvert.DeserializeObject<List<Motorista>>(responseString);

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult AtualizarListaSolicitacoes(string dataInicial, string dataFinal, int pagina = 1)
        {
            try
            {
                int QuantidadePorPagina = 20;
                int intervalo = (pagina * QuantidadePorPagina) - QuantidadePorPagina;

                DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];
                var token = dados.Token;

                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };

                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                FiltroSolicitacaoTransporteModel filtros = new FiltroSolicitacaoTransporteModel()
                {
                    DataInicial = dataInicial,
                    DataFinal = dataFinal,
                    IdCliente = Guid.Empty,
                    Offset = intervalo,
                    Limit = QuantidadePorPagina,
                    Status = Enumeradores.StatusSolicitacao.Pendente
                };

                var serializeContent = JsonConvert.SerializeObject(filtros);
                HttpContent content = new StringContent(serializeContent, Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{UrlApi.RetornarUrlWebApi()}api/ListaSolicitacoes", content, new CancellationToken()).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;

                API.Request.CheckRequest(response.StatusCode, responseString);

                ListaSolicitacaoTransporteModel listaComQuantidade = JsonConvert.DeserializeObject<ListaSolicitacaoTransporteModel>(responseString);

                List<SolicitacaoTransporteModel> lista = listaComQuantidade.ListaModel;
                lista = lista.Where(l => l.Status == Enumeradores.StatusSolicitacao.Pendente).ToList();

                foreach (var item in lista)
                {
                    item.DataEntregaTexto = item.DataEntrega.ToString("dd/MM/yyyy");
                }

                int totalPaginas = listaComQuantidade.Quantidade / QuantidadePorPagina;
                int sobraQuantidade = listaComQuantidade.Quantidade % QuantidadePorPagina;

                if (sobraQuantidade > 0)
                {
                    totalPaginas++;
                }

                return Json(new { Lista = lista, TotalPaginas = totalPaginas }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult SolicitarColeta(Guid idSolicitacao, string placaVeiculo, string registroMotorista)
        {
            try
            {
                DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];
                var token = dados.Token;

                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                SolicitacaoColetaModel model = new SolicitacaoColetaModel()
                {
                    IdSolicitacaoTransporte = idSolicitacao,
                    PlacaVeiculo = placaVeiculo,
                    RegistroMotorista = registroMotorista
                };

                var serializeContent = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(serializeContent, Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{UrlApi.RetornarUrlWebApi()}api/SolicitacaoColeta", content, new CancellationToken()).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                API.Request.CheckRequest(response.StatusCode, responseString);

                return Json(new { Erro = false, Mensagem = "Coleta solicitada com sucesso."}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Erro = true, Mensagem = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}