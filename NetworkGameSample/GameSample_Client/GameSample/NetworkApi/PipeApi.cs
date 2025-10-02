using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace GameSample.NetworkApi
{
    class PipeApi : INetworkApi
    {
        NamedPipeClientStream client;
        public void Connect(string address, int client_id)
        {
            client = new NamedPipeClientStream(address, "testpipe_"+client_id.ToString(), PipeDirection.InOut);
            client.Connect();
        }

        public void Disconnect()
        {
            client.Close();
            client.Dispose();            
        }

        public async Task ReceiveAsync(byte[] data)
        {
            await client.ReadAsync(data, 0, data.Length);
        }

        public async Task SendAsync(byte[] data)
        {            
            await client.WriteAsync(data,0,data.Length);           
        }
    }
}
