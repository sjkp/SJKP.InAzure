namespace SJKP.InAzure.Web.Controllers
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Linq;
    using Letsencrypt.AzureUsage;
    using System;
    [RoutePrefix("api/v1/search")]
    public class SearchController : ApiController
    {
        private static List<string> history = new List<string>();


        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(SearchInputModel model)
        {
            var response = new SearchResult()
            {
                MatchedSearchTerm = model.Term,
            }; 
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                history.Insert(0, model.Term);
                history = history.Take(5).ToList();
                var res = await Functions.GetAzureInformation(model.Term);
                if (res != null)
                {
                    response.Found = true;
                    response.Properties.Add("Region", res.Region);
                    response.ServiceType = "Azure";
                }
                else
                {
                    res = await Functions.GetOffice365Information(model.Term);
                    if (res != null)
                    {
                        response.Found = true;
                        response.ServiceType = "Office 365";
                        response.Properties.Add("Service", res.Office365);
                    }
                }
                
                
            }
            catch(Exception e)
            {
                throw;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("history")]
        public async Task<IHttpActionResult> GetHistory()
        {
            return Ok(await Task.FromResult(history));
        }
    }
}
