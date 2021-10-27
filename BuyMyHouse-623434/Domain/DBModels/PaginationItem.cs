using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DBModels
{
    public class PaginationItem<T>
    {
        public List<T> Items { get; set; }
        public string ContinuationToken { get; set; }
    }
}
