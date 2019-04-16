using Newtonsoft.Json;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.ExternalService
{
    public class SistemaGestaoFrotaExternalService : ISistemaGestaoFrotaExternalService
    {
        public List<Veiculo> RetornarVeiculosDisponiveis()
        {
            try
            {
                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync("http://localhost:55193/api/Veiculo", new CancellationToken()).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                List<Veiculo> lista = JsonConvert.DeserializeObject<List<Veiculo>>(responseString);

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Motorista> RetornarMotoristasDisponiveis()
        {
            try
            {
                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync("http://localhost:55193/api/Motorista", new CancellationToken()).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                List<Motorista> lista = JsonConvert.DeserializeObject<List<Motorista>>(responseString);

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}