using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.API
{
    public class UrlApi
    {
        public static string RetornarUrlWebApi()
        {
            return "http://localhost:53714/";
        }

        public static string RetornarUrlWebApiGestaoFrotas()
        {
            return "http://localhost:55193/";
        }
    }
}