using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DBModels
{
    public class BuyerInfo
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string BlobId { get; set; }
        public float YearlyIncome { get; set; }
    }
}
