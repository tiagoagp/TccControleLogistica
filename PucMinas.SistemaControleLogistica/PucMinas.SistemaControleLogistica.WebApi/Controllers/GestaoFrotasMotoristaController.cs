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
    public class GestaoFrotasMotoristaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            ISistemaGestaoFrotaService sistemaGestaoFrotaService = ServiceFactory.RetornarSistemaGestaoFrotaService();

            List<Motorista> lista = sistemaGestaoFrotaService.RetornarMotoristasDisponiveis();
            List<MotoristaModel> listaModel = RetornarListaModel(lista);

            return Ok(lista);
        }

        private List<MotoristaModel> RetornarListaModel(List<Motorista> lista)
        {
            List<MotoristaModel> listaModel = new List<MotoristaModel>();

            foreach (Motorista item in lista)
            {
                listaModel.Add(new MotoristaModel()
                {
                    Nome = item.Nome,
                    RegistroFuncionario = item.RegistroFuncionario
                });
            }

            return listaModel;
        }
    }
}
