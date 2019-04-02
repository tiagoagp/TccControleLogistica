using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.ExternalService
{
    public class ApiGoogleExternalService
    {
        public double RetornarDistanciaEntreDoisLocais(string cidadeOrigem, string ufOrigem, string cidadeDestino, string ufDestino)
        {
            try
            {
                var client = new HttpClient { Timeout = new TimeSpan(0, 5, 0) };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = client.GetAsync($"https://maps.googleapis.com/maps/api/directions/json?key=chaveapigoogle&origin={cidadeOrigem}-{ufOrigem}&destination={cidadeDestino}-{ufDestino}&sensor=false", new CancellationToken()).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                var dynObject = JsonConvert.DeserializeObject<dynamic>(responseString);
                var dynObjectRoutes = dynObject.routes;
                var dynObjectRoot = dynObjectRoutes[0];
                var dynObjectLegs = dynObjectRoot.legs;
                var dynObjectPreDistance = dynObjectLegs[0];
                var dynObjectDistance = dynObjectPreDistance.distance;
                var dybObjectValor = dynObjectDistance.value;

                double distancia;
                double.TryParse(dybObjectValor.ToString(), out distancia);

                return distancia;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}