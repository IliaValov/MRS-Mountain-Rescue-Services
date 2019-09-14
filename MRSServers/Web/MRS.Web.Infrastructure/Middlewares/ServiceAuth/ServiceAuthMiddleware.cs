using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MRS.Web.Infrastructure.Middlewares.ServiceAuth
{
    public class ServiceAuthMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptions<ServiceAuthOptions> options;
        private readonly Func<HttpContext, Task<GenericPrincipal>> principalResolver;

        public ServiceAuthMiddleware(
            RequestDelegate next,
            IOptions<ServiceAuthOptions> options,
            Func<HttpContext, Task<GenericPrincipal>> principalResolver)
        {
            this.next = next;
            this.options = options;
            this.principalResolver = principalResolver;
        }
    }
}
