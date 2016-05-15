using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJKP.InAzure.Web.Models
{
    public class SearchResult
    {
        public SearchResult()
        {
            this.Properties = new Dictionary<string, object>();
        }
        public bool Found { get; internal set; }
        public string MatchedSearchTerm { get; set; }
        public Dictionary<string,object> Properties{ get; set; }
        public string ServiceType { get; set; }
    }
}
