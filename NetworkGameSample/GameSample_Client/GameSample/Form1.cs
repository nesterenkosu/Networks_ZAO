using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using FromBytesToBytes;
using System.IO;
using System.IO.Pipes;
using GameSample.NetworkApi;

namespace GameSample
{
    struct GameMove
    {
        public int x, y;
    }
    public partial class Form1 : Form
    {
        byte[] gameboard;

        GameFieldView[,] gameboard_view = new GameFieldView[3, 3];

        byte myfigure;

        NetworkStream network_stream;

        byte[] buffer = new byte[255];

        TcpClient client;

        StreamReader pipe_reader;
        StreamWriter pipe_writer;

        INetworkApi network_api;
        public Form1()
        {
            InitializeComponent();
            gameboard = new byte[9];

            //Заполнение поля пустыми клетками
            for (int i = 0; i < 9; i++)
                gameboard[i] = 100;

            //Создание визуализации игрового поля
            GenerateFieldView();
        }

        private bool _mymove;
        bool mymove
        {
            get {
                return _mymove;
            }
            set {
                _mymove = value;
                lb_movestate.Text = _mymove ? "Ваш ход" : "Ход противника";
                gb_gameboardview.Enabled = _mymove;
            }
        }

        private async void WaitForServer()
        {
            //string message;

            //byte[] data = new byte[255];

           // while (true)
            //{
                //Принятие текущего состояния поля                
                //await network_stream.ReadAsync(gameboard, 0, gameboard.Length);
                await network_api.ReceiveAsync(gameboard);

                mymove = true;

                //Перерисовка поля
                RefreshBoardView();
           // }
        }

        public async void SendMoveAsync(int x, int y)
        {
            byte[] gamemove_bytes = new byte[255];

            GameMove gamemove = new GameMove();
            gamemove.x = x;
            gamemove.y = y;

            //Отправка хода

            //перекодирование в массив байт
            gamemove_bytes=MyEncoder.getBytes<GameMove>(gamemove);

            //собственно отправка в сеть
            //await network_stream.WriteAsync(gamemove_bytes,0, gamemove_bytes.Length);
            
            Console.WriteLine("Отправляем...");

            await network_api.SendAsync(gamemove_bytes);

            Console.WriteLine("Отправили.");

            InvokeMove(gamemove, myfigure);
            RefreshBoardView();

            mymove = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Выбрать протокол TCP
            network_api = new TcpApi();

            //Установка подключения к серверу
            /*client = new TcpClient();
            client.Connect(IPAddress.Parse(tb_server_ip.Text), 8888);
            network_stream = client.GetStream();*/
            
            network_api.Connect(tb_server_address.Text);
           
            write_log("Подключение к серверу установлено");
            write_log("Ожидание подключения всех игроков...");

            //Ожидание начала игры
            WaitForGameStart();

            //Запуск главного цикла ожидания сообщений сервера
            //WaitForServer();
            
        }        

        string GetGameFieldView(int index)
        {
            switch(gameboard[index])
            {
                case 0: return "O";
                case 1: return "X";
            }

            return " ";
        }

        void RefreshBoardView()
        {
            for(int i=0;i<3;i++)
                for (int j = 0; j < 3; j++)
                    gameboard_view[i,j].Text = GetGameFieldView(i*3+j);            
        }

        void write_log(string message)
        {
            lb_logs.Items.Add(message);
        }

        void InvokeMove(GameMove move, byte figure)
        {
            if (gameboard[move.x * 3 + move.y] == 100)
                gameboard[move.x * 3 + move.y] = figure;
        }

        async void WaitForGameStart()
        {
            //Ожидание подключения всех игроков и получения собственной роли

            //Ожидание от сервера игровой очереди и роли игрока (X или O)
            byte[] role = new byte[255];
            //await network_stream.ReadAsync(role, 0, 1);
            await network_api.ReceiveAsync(role);

            Console.Write("Принято в игре: ");
            Console.WriteLine(role[0]);

            write_log("Все игроки подключились. Игра началась");

            //Определим - ваш ход или нет
            mymove = (role[0] == 0);

            myfigure = role[0];

            //Отобразим Вашу игровую роль
            if (mymove)
            {
                lb_prompt.Text = "Вы играете за O";
            }
            else
            {
                lb_prompt.Text = "Вы играете за X";
            }

            if(!mymove)
            {
                //Ход "не наш" - значит это второй клиент
                WaitForServer();
            }
        }

        void GenerateFieldView()
        {
            
            for(int i=0;i<3;i++)
                for (int j = 0; j < 3; j++)
                {
                    //Динамическое создание кнопки (клетки поля)
                    //с заданными параметрами
                    gameboard_view[i, j] = new GameFieldView
                    {
                        //Стандартные свойства объекта Button
                        Width = 60,
                        Height = 60,
                        Top = 15 + i * 60,
                        Left = 10 + j * 60,
                        //Свойства объекта GameBoardView, хранящие координаты клетки поля
                        i=i,
                        j=j
                    };

                    //Назначение обработчика нажатия на кнопку (клетку поля)
                    gameboard_view[i, j].Click += GameFieldClicked;

                    //Добавление созданной кнопки внутрь GroupBox
                    gb_gameboardview.Controls.Add(gameboard_view[i, j]);
                }
        }

        void GameFieldClicked(object sender, EventArgs e)
        {
            int i = (sender as GameFieldView).i; 
            int j = (sender as GameFieldView).j;

            SendMoveAsync(i, j);

            WaitForServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Выбрать протокол PIPE
            network_api = new PipeApi();

            //Выполнить подключение
            network_api.Connect(tb_server_address.Text, Convert.ToInt32(tb_client_id.Text));

            write_log("Подключение к серверу установлено");
            write_log("Ожидание подключения всех игроков...");

            //Ожидание начала игры
            WaitForGameStart();

            //Запуск главного цикла ожидания сообщений сервера
            //WaitForServer();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            network_api.Disconnect();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Выбрать протокол UDP
            network_api = new UdpApi();

            //Выполнить подключение
            network_api.Connect(tb_server_address.Text, Convert.ToInt32(tb_client_id.Text));

            write_log("Подключение к серверу установлено");
            write_log("Ожидание подключения всех игроков...");

            //Ожидание начала игры
            WaitForGameStart();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //Выбрать протокол MailSlot
            network_api = new MailslotApi();

            //Выполнить подключение
            network_api.Connect(tb_server_address.Text, Convert.ToInt32(tb_client_id.Text)-1);

            write_log("Подключение к серверу установлено");
            write_log("Ожидание подключения всех игроков...");

            //Ожидание начала игры
            WaitForGameStart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Выбрать протокол MailSlot
            network_api = new InternetApi();

            //Выполнить подключение
            network_api.Connect("http://gameserver.ru:8888/", Convert.ToInt32(tb_client_id.Text)-1);

            write_log("Подключение к серверу установлено");
            write_log("Ожидание подключения всех игроков...");

            //Ожидание начала игры
            WaitForGameStart();
        }
    }

    class GameFieldView: Button
    {
        private int _i, _j;

        public int i
        {
            get
            {
                return _i;
            }

            set
            {
                _i = value;
            }
        }

        public int j
        {
            get
            {
                return _j;
            }

            set
            {
                _j = value;
            }
        }
    }
}
