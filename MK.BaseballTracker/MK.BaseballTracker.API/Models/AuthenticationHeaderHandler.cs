using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MK.BaseballTracker.API.Models
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        
      protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                                                                      
        {
            IEnumerable<string> apiKeyHeaders = null;

            if (request.Headers.TryGetValues("x-apikey", out apiKeyHeaders))
            {
               
                var apiKeyHeader = apiKeyHeaders.FirstOrDefault();
                if (apiKeyHeader == "12345")
                {
                    
                    return base.SendAsync(request, cancellationToken);
                }
            }

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;

        }
        
    }
}