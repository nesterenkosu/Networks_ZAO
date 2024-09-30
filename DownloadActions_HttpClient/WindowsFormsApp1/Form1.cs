using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public async void getData()
        {
            //Подготовка к скачиванию акций - инициализация сетевых объектов
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(@"https://quote.rbc.ru/v5/ajax/catalog/get-tickers?type=share&sort=blue_chips&limit=200&offset=0");
            response.EnsureSuccessStatusCode();
			
			//Получение списка акций в формате JSON с веб-сайта 
            string data = await response.Content.ReadAsStringAsync();

			//Преобразование данных из формата JSON в массив объектов языка C#
            var parsedData = JsonSerializer.Deserialize<List<Action>>(data);  
			
			//Отображение полученного списка акций в виде таблицы на форме
            //(элемент управления DataGridView)
            foreach (var item in parsedData)
            {
                //вывод каждой акции в виде строки DataGridView
                this.dataGridView1.Rows.Add(item.company.title, item.title, item.price,item.currency);                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.getData();
        }
    }

	//Классы для представления на языке C# данных, скачанных с веб-сайта
    public class Company
    {
        public string title { get; set; } 
    }
    public class Action
    {
        public Company company { get; set; }
        public string title { get; set; } 
        public double? price { get; set; } 
        public string currency { get; set; }        
    }
}
