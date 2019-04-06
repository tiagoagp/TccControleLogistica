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
    //[Authorize(Roles = "Cliente")]
    public class SolicitacaoTransporteController : Controller
    {
        // GET: SolicitacaoTransporte
        public ActionResult Index()
        {
            DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];
            var token = dados.Token;

            var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            
            var response = client.GetAsync($"{UrlApi.RetornarUrlWebApi()}api/ListaSolicitacoes?dataInicial={DateTime.MinValue}&dataFinal={DateTime.MinValue}&idCliente={dados.UsuarioId}", new CancellationToken()).Result;
            
            var responseString = response.Content.ReadAsStringAsync().Result;

            API.Request.CheckRequest(response.StatusCode, responseString);

            List<SolicitacaoTransporteModel> lista = JsonConvert.DeserializeObject<List<SolicitacaoTransporteModel>>(responseString);

            return View(lista);
        }

        public ActionResult Create()
        {
            ViewBag.GravadoComSucesso = false;
            
            SolicitacaoTransporteModel model = new SolicitacaoTransporteModel();
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            ViewBag.GravadoComSucesso = false;

            DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];
            var token = dados.Token;

            var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = client.GetAsync($"{UrlApi.RetornarUrlWebApi()}api/SolicitacaoTransporte?id={id}", new CancellationToken()).Result;

            var responseString = response.Content.ReadAsStringAsync().Result;

            API.Request.CheckRequest(response.StatusCode, responseString);

            SolicitacaoTransporteModel model = JsonConvert.DeserializeObject<SolicitacaoTransporteModel>(responseString);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Inserir(SolicitacaoTransporteModel model)
        {
            ViewBag.GravadoComSucesso = false;

            if (model.Produtos.Count == 0)
            {
                ModelState.AddModelError("", "Informe pelo menos um produto para efetuar a abertura da solicitação de transporte.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];
                    var token = dados.Token;

                    var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    model.Usuario = new UsuarioModel();
                    model.Usuario.Id = dados.UsuarioId;

                    var serializeContent = JsonConvert.SerializeObject(model);
                    HttpContent content = new StringContent(serializeContent, Encoding.UTF8, "application/json");
                    var response = client.PostAsync($"{UrlApi.RetornarUrlWebApi()}api/SolicitacaoTransporte", content, new CancellationToken()).Result;
                    var responseString = response.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(responseString))
                    {
                        responseString = responseString.Replace("\"", string.Empty);
                    }

                    ViewBag.GravadoComSucesso = true;

                    API.Request.CheckRequest(response.StatusCode, responseString);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Erro ao gravar a solicitação: " + e.Message);
                }
            }

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult SimularFrete(FreteModel model)
        {
            try
            {
                double valor = 0;
                
                DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];
                var token = dados.Token;

                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };

                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var serializeContent = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(serializeContent, Encoding.UTF8, "application/json");

                var response = client.PostAsync($"{UrlApi.RetornarUrlWebApi()}api/SimuladorFrete", content, new CancellationToken()).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;

                API.Request.CheckRequest(response.StatusCode, responseString);
                
                return Json(new { Erro = false, Mensagem = "", Valor = responseString.Replace(".", ",") });
            }
            catch (Exception e)
            {
                return Json(new { Erro = true, Mensagem = e.Message, Valor = 0 });
            }
        }
    }
}