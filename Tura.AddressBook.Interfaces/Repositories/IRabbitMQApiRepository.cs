using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.AddressBook.Interfaces.Repositories
{
    public interface IRabbitMQApiRepository
    {
        void SendLocationReport();
    }
}
