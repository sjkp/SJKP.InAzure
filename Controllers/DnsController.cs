using Heijden.DNS;
using SJKP.InAzure.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SJKP.InAzure.Web.Controllers
{
    public class DnsController : ApiController
    {
        private readonly Resolver resolver;

        public DnsController()
        {
            this.resolver = new Resolver();
            this.resolver.DnsServer = "8.8.8.8";
            this.resolver.UseCache = false;
            this.resolver.TimeOut = 1000;
            this.resolver.TransportType = Heijden.DNS.TransportType.Tcp;
            this.resolver.Recursion = true;
            this.resolver.Retries = 3;
        }
        [HttpPost]
        public async Task<IHttpActionResult> Get(DnsLookup model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dnsResponse = resolver.Query(model.Hostname, QType.CNAME);
            var dnsResponseA = resolver.Query(model.Hostname, QType.A);


            return Ok(new
            {
                CName = dnsResponse.RecordsCNAME,
                A = dnsResponseA.RecordsA.Select(s => s.Address.ToString())
            });
        }
    }

    
}
