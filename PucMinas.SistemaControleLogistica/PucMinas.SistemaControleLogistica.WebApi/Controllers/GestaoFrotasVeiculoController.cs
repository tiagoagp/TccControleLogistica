using PucMinas.SistemaControleLogistica.Application;
using PucMinas.SistemaControleLogistica.Application.Factory;
using PucMinas.SistemaControleLogistica.Application.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PucMinas.SistemaControleLogistica.WebApi.Controllers
{
    public class GestaoFrotasVeiculoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            ISistemaGestaoFrotaService sistemaGestaoFrotaService = ServiceFactory.RetornarSistemaGestaoFrotaService();

            List<Veiculo> lista = sistemaGestaoFrotaService.RetornarVeiculosDisponiveis();
            List<VeiculoModel> listaModel = RetornarListaModel(lista);

            return Ok(lista);
        }

        private List<VeiculoModel> RetornarListaModel(List<Veiculo> lista)
        {
            List<VeiculoModel> listaModel = new List<VeiculoModel>();

            foreach (Veiculo item in lista)
            {
                listaModel.Add(new VeiculoModel()
                {
                    Modelo = item.Modelo,
                    Placa = item.Placa
                });
            }

            return listaModel;
        }
    }
}
