using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJKP.InAzure.Web.Models
{
    public class SearchResult
    {
        public bool Found { get; internal set; }
        public string MatchedSearchTerm { get; set; }
        public string Region { get; set; }
    }
}
