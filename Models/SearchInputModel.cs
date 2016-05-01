using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJKP.InAzure.Web.Models
{
    public class SearchInputModel
    {
        [Required]
        public string Term { get; set; }
    }
}
