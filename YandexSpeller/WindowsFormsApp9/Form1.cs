using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyYandexSpeller.SpellService ys = new MyYandexSpeller.SpellService();
            MyYandexSpeller.SpellError[] errors = ys.checkText("Туст", "ru", 0, "");
            if (errors.Length == 0)
                MessageBox.Show("Текст не содержит ошибок");
            else
            {
                MessageBox.Show("Текст содержит ошибки");
                foreach (MyYandexSpeller.SpellError error in errors)
                {
                    MessageBox.Show("В слове [" + error.word + "]");
                    foreach(string var in error.s)
                    {
                        MessageBox.Show("Вариант исправления: "+var);
                    }
                }
                
            }
        }
    }
}
