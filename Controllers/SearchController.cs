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
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                history.Insert(0, model.Term);
                history = history.Take(5).ToList();
                var res = Functions.GetAzureInformation(model.Term);
                if (res == null)
                    return Ok(new SearchResult()
                    {                        
                        MatchedSearchTerm = model.Term,                     
                    });

                return Ok(new SearchResult()
                {
                    Found = true,
                    MatchedSearchTerm = model.Term,
                    Region = res.Region
                });
            }
            catch(Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("history")]
        public async Task<IHttpActionResult> GetHistory()
        {
            return Ok(await Task.FromResult(history));
        }
    }
}
