using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Dtos.Base
{
    public class FilterDto
    {
        public string OrderBy { get; set; }
        public bool Asc  { get; set; }
    }
}
