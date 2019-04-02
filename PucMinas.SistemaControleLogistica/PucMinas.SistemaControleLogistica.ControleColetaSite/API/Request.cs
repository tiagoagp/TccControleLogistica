using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.API
{
    public static class Request
    {
        public static void CheckRequest(HttpStatusCode httpStatus, string responseString)
        {
            if (httpStatus != HttpStatusCode.OK)
            {
                switch (httpStatus)
                {
                    case HttpStatusCode.Forbidden: throw new UnauthorizedAccessException();
                    case HttpStatusCode.Accepted: throw new ApplicationException(responseString.Message());
                    default: throw new Exception(responseString.Message());
                }
            }
        }

        public static string Message(this string responseString)
        {
            try
            {
                return JsonConvert.DeserializeObject<Error>(responseString).Message;
            }
            catch (Exception)
            {
                return responseString;
            }
        }
    }

    public class Error
    {
        public string Message { get; set; }
    }
}