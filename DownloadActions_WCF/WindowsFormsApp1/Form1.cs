using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			//Генерация прокси-объекта для доступа к REST API 
			//веб-сайта с акциями rbc.ru
            ChannelFactory<RBC_API> rbc_api = new ChannelFactory<RBC_API>("RBC_ENDPOINT");
            var proxy = rbc_api.CreateChannel();

			//Получение списка акций с веб-сайта
            Action[] actions = proxy.GetActions();
			
			//Вывод полученного списка акций в таблицу на форме
            foreach(Action action in actions)
            {
                dataGridView1.Rows.Add(action.company.title, action.title, action.currency,action.price);
            }
        }
    }

    [ServiceContract]
    [DataContractFormat]
    public interface RBC_API
    {
		//Сопоставление метода GetActions с методом REST API веб-сайта с акциями
        [WebGet(
            UriTemplate = "/v5/ajax/catalog/get-tickers?type=share&sort=blue_chips&limit=200&offset=0",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json
        )]
        Action[] GetActions();
    }

	//Контракты данных для представления на языке C# данных, скачанных с веб-сайта
    [DataContract]
    public class Company
    {
        [DataMember]
        public string title { get; set; }
    }

    [DataContract]
    public class Action
    {
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public double? price { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public Company company { get; set; }
    }
}
