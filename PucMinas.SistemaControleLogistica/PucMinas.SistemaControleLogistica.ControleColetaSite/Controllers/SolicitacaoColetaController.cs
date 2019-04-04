using PucMinas.SistemaControleLogistica.ControleColetaSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.Controllers
{
    public class SolicitacaoColetaController : Controller
    {
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
                List<Veiculo> lista = new List<Veiculo>();

                lista.Add(new Veiculo() { Placa = "IPX4456", Modelo = "Ford Ranger" });
                lista.Add(new Veiculo() { Placa = "SSK7848", Modelo = "Picape Sei Lá" });
                lista.Add(new Veiculo() { Placa = "IPT9902", Modelo = "XSaara Pixasso" });
                lista.Add(new Veiculo() { Placa = "RSR1123", Modelo = "Ford Fiesta" });
                lista.Add(new Veiculo() { Placa = "LLT7876", Modelo = "Honda Civis" });

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
                List<Motorista> lista = new List<Motorista>();

                lista.Add(new Motorista() { Nome = "João Paulo", RegistroFuncionario = "RX224" });
                lista.Add(new Motorista() { Nome = "Pedro Albuquerque", RegistroFuncionario = "RT456" });
                lista.Add(new Motorista() { Nome = "Jose Farias", RegistroFuncionario = "RJ212" });
                lista.Add(new Motorista() { Nome = "Soares Mendonça", RegistroFuncionario = "JR295" });

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult AtualizarListaSolicitacoes(string dataInicial, string dataFinal)
        {
            try
            {
                List<SolicitacaoTransporteModel> lista = new List<SolicitacaoTransporteModel>();

                lista.Add(new SolicitacaoTransporteModel() { Id = Guid.NewGuid(), CidadeDestino = "Caxias do Sul", EstadoDestino = "RS", DataEntregaTexto = "10/08/2019", Usuario = new UsuarioModel() { NomeUsuario = "Empresa Teste 1" } });
                lista.Add(new SolicitacaoTransporteModel() { Id = Guid.NewGuid(), CidadeDestino = "Flores da Cunha", EstadoDestino = "RS", DataEntregaTexto = "15/09/2021", Usuario = new UsuarioModel() { NomeUsuario = "Empresa Teste 2" } });
                lista.Add(new SolicitacaoTransporteModel() { Id = Guid.NewGuid(), CidadeDestino = "São Paulo", EstadoDestino = "SP", DataEntregaTexto = "04/03/2021", Usuario = new UsuarioModel() { NomeUsuario = "Empresa Teste 3" } });

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult SolicitarColeta(Guid idSolicitacao, string placaVeiculo, string registroFuncionario)
        {
            try
            {
                return Json(new { Erro = false, Mensagem = ""}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Erro = true, Mensagem = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}