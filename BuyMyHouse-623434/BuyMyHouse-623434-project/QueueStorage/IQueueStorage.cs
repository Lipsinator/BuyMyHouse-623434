using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse_623434_project.QueueStorage
{
    public interface IQueueStorage
    {
        Task CreateMessage(string message);
        Task<string> PeekMessage();
        Task DeleteMessage();

    }
}
