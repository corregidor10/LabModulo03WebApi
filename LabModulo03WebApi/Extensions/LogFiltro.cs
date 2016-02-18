using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using LabModulo03WebApi.Utils;

namespace LabModulo03WebApi.Extensions
{
    public class LogFiltro : Attribute, IActionFilter
    {
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {

            Utilidades.EscribirLog("*******EMPIEZA EL FILTRO********");

            foreach (var item in actionContext.ActionArguments.Keys)
                Utilidades.EscribirLog(string.Format("{0} : {1}", item, actionContext.ActionArguments[item]));
            var response = await continuation();
            Utilidades.EscribirLog(string.Format("La respuesta: {0}", response));
            return response;


        }

        public bool AllowMultiple { get { return true; } }
    }
}