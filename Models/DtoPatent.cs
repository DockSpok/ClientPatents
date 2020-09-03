using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPatents.Models
{
    public class DtoPatent
    {
        public int PatentId { get; set; }
        public string PatentNumber { get; set; }
        public string PatentTitle { get; set; }
        public int PatentNumClaims { get; set; }
        public List<string> PatentClaims { get; set; }
        public DateTime PatentDate { get; set; }
    }
}
