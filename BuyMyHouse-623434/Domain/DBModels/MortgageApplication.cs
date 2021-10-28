using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DBModels
{
    public class MortgageApplication
    {
        public string id { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public float AmountToBorrow { get; set; }
    }
}
