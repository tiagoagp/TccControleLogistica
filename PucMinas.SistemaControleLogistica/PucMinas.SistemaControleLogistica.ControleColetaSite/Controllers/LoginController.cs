using Newtonsoft.Json;
using PucMinas.SistemaControleLogistica.ControleColetaSite.API;
using PucMinas.SistemaControleLogistica.ControleColetaSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.MensagemErro = string.Empty;

            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(string usuario, string senha, string redirectUrl)
        {
            try
            {
                try
                {
                    Logar(usuario, senha);

                    DadosUsuarioAutenticado dados = (DadosUsuarioAutenticado)Session["usuario"];

                    var identity = new ClaimsIdentity("ApplicationCookie");
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    identity.AddClaim(new Claim(ClaimTypes.Email, usuario));

                    if (dados.Tipo == Enumeradores.TipoUsuario.Cliente)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Cliente"));
                    }

                    if (dados.Tipo == Enumeradores.TipoUsuario.Transportadora)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Transportadora"));
                    }

                    authManager.SignIn(identity);

                    return Redirect(GetRedirectUrl(redirectUrl));
                }
                catch (Exception e)
                {
                    ViewBag.MensagemErro = e.Message;
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetRedirectUrl(string redirectUrl)
        {
            if (string.IsNullOrEmpty(redirectUrl) || !Url.IsLocalUrl(redirectUrl) || redirectUrl == "/")
            {
                return Url.Action("Index", "Home");
            }

            return redirectUrl;
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");

            Session["usuario"] = null;

            return RedirectToAction("Index");
        }

        public void Logar(string usuario, string senha)
        {
            var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("username", usuario));
            nvc.Add(new KeyValuePair<string, string>("password", senha));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));

            var result = client.PostAsync($"{UrlApi.RetornarUrlWebApi()}api/token", new FormUrlEncodedContent(nvc), new CancellationToken()).Result;
            var returnstr = result.Content.ReadAsStringAsync().Result;

            if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new System.ApplicationException("Algo deu errado ao enviar as informações de login, tente novamente!");
            }

            var dynObject = JsonConvert.DeserializeObject<dynamic>(returnstr);
            var dicObjs = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnstr);

            if ((dicObjs != null) && (dicObjs.ContainsKey("error")))
            {
                throw new System.ApplicationException("Não foi possível efetuar a autenticação");
            }

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {dynObject.access_token.Value}");

            result = client.GetAsync($"{UrlApi.RetornarUrlWebApi()}api/usuario", new CancellationToken()).Result;

            returnstr = result.Content.ReadAsStringAsync().Result;

            UsuarioModel userModel = JsonConvert.DeserializeObject<UsuarioModel>(returnstr);

            if (userModel == null)
            {
                throw new ApplicationException("Ocorreu algum erro na autenticação. Tente novamente.");
            }

            DadosUsuarioAutenticado dados = new DadosUsuarioAutenticado()
            {
                Token = dynObject.access_token.Value,
                NomeUsuario = userModel.NomeUsuario,
                Email = userModel.Email,
                UsuarioId = userModel.Id,
                Tipo = userModel.Tipo
            };

            Session["usuario"] = dados;
        }
    }
}