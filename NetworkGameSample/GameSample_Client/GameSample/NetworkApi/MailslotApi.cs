using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GameSample.NetworkApi
{
    class MailslotApi : INetworkApi
    {
        const string MailSlotServer1_Name = @"\\.\mailslot\MyMailSlotServer1";
        const string MailSlotServer2_Name = @"\\.\mailslot\MyMailSlotServer2";
        const string MailSlotClient1_Name = @"\\.\mailslot\MyMailSlotClient1";
        const string MailSlotClient2_Name = @"\\.\mailslot\MyMailSlotClient2";

        //Создание нового мэйлслота
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateMailslot(string lpName, uint nMaxMessageSize, uint nMaxInstances, IntPtr lpSecurityAttributes);
        //Подключение к существующему мэйлслоту
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);

        IntPtr mailslot_server;
        IntPtr mailslot_client;

        public void Connect(string address, int client_id = 0)
        {
            //Создание клиентского мэйлслота
            string MailSlotClient_Name = @"\\.\mailslot\MyMailSlotClient" + (client_id+1).ToString();

            //MessageBox.Show(MailSlotClient_Name);

            mailslot_client = CreateMailslot(MailSlotClient_Name, 0, 0, IntPtr.Zero);
            if (mailslot_client == IntPtr.Zero)
            {
                throw new Exception("Ошибка создания клиентского мэйлслота.");
            }

            //Подключение к серверному мэйлслоту
            string MailSlotServer_Name = @"\\"+address+@"\mailslot\MyMailSlotServer" + (client_id+1).ToString();
            mailslot_server=CreateFile(MailSlotServer_Name, 0x40000000, 0, IntPtr.Zero, 3, 0, IntPtr.Zero);
            

            //Отправка на сервер запрос на подключение
            byte[] buffer = new byte[255];
            buffer[0] = 1;
            SendAsync(buffer);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(byte[] data)
        {
            byte[] buffer = new byte[512];
            uint bytesRead;

            // Асинхронное чтение из мэйлслота
            return Task.Run(() =>
            {
                while (!ReadFile(mailslot_client, data, (uint)buffer.Length, out bytesRead, IntPtr.Zero))
                    System.Threading.Thread.Sleep(100);

            });
        }

        public Task SendAsync(byte[] data)
        {
            uint bytesWritten;

            // Асинхронная отправка сообщения
            return Task.Run(() =>
            {
                WriteFile(mailslot_server, data, (uint)data.Length, out bytesWritten, IntPtr.Zero);


            });
        }
    }
}
