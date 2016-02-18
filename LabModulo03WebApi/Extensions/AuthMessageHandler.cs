using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace LabModulo03WebApi.Extensions
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private GenericPrincipal AuthenticateUser(string username, string password)
        {
            if (username == password)
            {
                IIdentity identity = new GenericIdentity(username);
                var principal = new GenericPrincipal(identity, new[] {"Users","Admins"});
                return principal;
            }
            return null;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;

            if (request.Headers.Authorization !=null && request.Headers.Authorization.Scheme=="Basic" )
            {
                var datosCodificados = request.Headers.Authorization.Parameter.Trim();

                var userpass = Encoding.Default.GetString(Convert.FromBase64String(datosCodificados));

                var datos = userpass.Split(":".ToCharArray());

                var login = datos[0];

                var password = datos[1];

                GenericPrincipal principal = AuthenticateUser(login, password);

                if (principal==null)
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized);

                    response.Headers.Add("WWW-Authenticate", "Basic");

                    return response;
                }

                else
                {
                    request.GetRequestContext().Principal = principal;
                }


            }

            response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode==HttpStatusCode.Unauthorized)
            {
                response.Headers.Add("WWW-Authenticate", "Basic");
            }
            return response;
        }
    }
}