using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSample.NetworkApi
{
    interface INetworkApi
    {
        void Connect(string address, int client_id=0);

        Task SendAsync(byte[] data);

        Task ReceiveAsync(byte[] data);

        void Disconnect();

    }
}
