using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Interfaces
{
    public interface IApiGoogleExternalService
    {
        double RetornarDistanciaEntreDoisLocais(string cidadeOrigem, string ufOrigem, string cidadeDestino, string ufDestino);

    }
}