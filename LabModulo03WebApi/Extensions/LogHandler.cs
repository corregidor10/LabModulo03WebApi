using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using LabModulo03WebApi.Utils;

namespace LabModulo03WebApi.Extensions
{
    public class LogHandler:DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Utilidades.EscribirLog("Petición--------->>> "+request.ToString());
            var response = await base.SendAsync(request, cancellationToken);

            Utilidades.EscribirLog("Respuesta-------->>> " + response.ToString());

            return response;
        }
    }
} 