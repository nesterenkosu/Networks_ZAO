using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace GameSample.NetworkApi
{
    class InternetApi : INetworkApi
    {
        private string global_url;
        private int client_id;

        public async void Connect(string address, int client_id = 0)
        {
            this.global_url = address;
            this.client_id = client_id;

            byte[] data = new byte[1];
            //data[0] = client_id;

            await SendAsync(data);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(byte[] data)
        {
            //int l = (length == 0) ? data.Length : length;
            // Convert.ToBase64String(data, 0, l);

            
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            string response_as_str;
            byte[] response_as_bytes;
            do
            {
                //Подключение к глобальному серверу и принятие его ответа
                response = await client.GetAsync($@"{global_url}?program_id={client_id}&receive");
                response.EnsureSuccessStatusCode();
                //Получение принятых данных в виде текстовой строки
                response_as_bytes = await response.Content.ReadAsByteArrayAsync();
                response_as_str = Encoding.UTF8.GetString(response_as_bytes);

                Thread.Sleep(100);

            } while (response_as_str.Trim() == "");

            //преобразование принятых данных из base64 в поток байт
            byte[] temp = Convert.FromBase64String(response_as_str);
            Array.Copy(temp, data, temp.Length);
            //data = Convert.FromBase64String(response_as_str);
            //data[0] = 11;
            Console.WriteLine("Принято str: "+response_as_str);
            Console.WriteLine("Принято byte: " + data[0]);
        }

        public async Task SendAsync(byte[] data)
        {
            HttpClient client = new HttpClient();

            StringContent content = new StringContent(Convert.ToBase64String(data, 0, data.Length));

            // определяем данные запроса
            var request = new HttpRequestMessage(HttpMethod.Post, $@"{global_url}?program_id={client_id}&send&server");

            // установка отправляемого содержимого
            request.Content = content;

            // отправляем запрос
            var response = await client.SendAsync(request);
        }
    }
}
