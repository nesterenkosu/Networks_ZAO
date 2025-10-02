
namespace GameSample
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_server_address = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_logs = new System.Windows.Forms.ListBox();
            this.button11 = new System.Windows.Forms.Button();
            this.lb_movestate = new System.Windows.Forms.Label();
            this.lb_prompt = new System.Windows.Forms.Label();
            this.gb_gameboardview = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_client_id = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.lb_status = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_server_address
            // 
            this.tb_server_address.Location = new System.Drawing.Point(257, 49);
            this.tb_server_address.Name = "tb_server_address";
            this.tb_server_address.Size = new System.Drawing.Size(100, 22);
            this.tb_server_address.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(396, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(201, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Подключиться по TCP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Адрес сервера";
            // 
            // lb_logs
            // 
            this.lb_logs.FormattingEnabled = true;
            this.lb_logs.ItemHeight = 16;
            this.lb_logs.Location = new System.Drawing.Point(396, 219);
            this.lb_logs.Name = "lb_logs";
            this.lb_logs.Size = new System.Drawing.Size(201, 228);
            this.lb_logs.TabIndex = 4;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(396, 463);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(201, 32);
            this.button11.TabIndex = 5;
            this.button11.Text = "Отключиться";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // lb_movestate
            // 
            this.lb_movestate.AutoSize = true;
            this.lb_movestate.Location = new System.Drawing.Point(40, 482);
            this.lb_movestate.Name = "lb_movestate";
            this.lb_movestate.Size = new System.Drawing.Size(0, 17);
            this.lb_movestate.TabIndex = 6;
            // 
            // lb_prompt
            // 
            this.lb_prompt.AutoSize = true;
            this.lb_prompt.Location = new System.Drawing.Point(40, 448);
            this.lb_prompt.Name = "lb_prompt";
            this.lb_prompt.Size = new System.Drawing.Size(0, 17);
            this.lb_prompt.TabIndex = 7;
            // 
            // gb_gameboardview
            // 
            this.gb_gameboardview.AutoSize = true;
            this.gb_gameboardview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gb_gameboardview.Location = new System.Drawing.Point(40, 106);
            this.gb_gameboardview.Name = "gb_gameboardview";
            this.gb_gameboardview.Size = new System.Drawing.Size(6, 21);
            this.gb_gameboardview.TabIndex = 9;
            this.gb_gameboardview.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Номер клиента (для pipe)";
            // 
            // tb_client_id
            // 
            this.tb_client_id.Location = new System.Drawing.Point(257, 18);
            this.tb_client_id.Name = "tb_client_id";
            this.tb_client_id.Size = new System.Drawing.Size(100, 22);
            this.tb_client_id.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(396, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(201, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Подключиться по PIPE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            this.lb_status.Location = new System.Drawing.Point(396, 474);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(0, 17);
            this.lb_status.TabIndex = 13;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(396, 79);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(201, 27);
            this.button4.TabIndex = 14;
            this.button4.Text = "Подключиться по UDP";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(396, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(201, 29);
            this.button3.TabIndex = 15;
            this.button3.Text = "Подключиться по MailSlot";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(396, 148);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(201, 29);
            this.button5.TabIndex = 15;
            this.button5.Text = "Подключиться по Интернет";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 536);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lb_status);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tb_client_id);
            this.Controls.Add(this.gb_gameboardview);
            this.Controls.Add(this.lb_prompt);
            this.Controls.Add(this.lb_movestate);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.lb_logs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_server_address);
            this.Name = "Form1";
            this.Text = "Игровая программа \"Крестики-нолики\" (клиент)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_server_address;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lb_logs;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label lb_movestate;
        private System.Windows.Forms.Label lb_prompt;
        private System.Windows.Forms.GroupBox gb_gameboardview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_client_id;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lb_status;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
    }
}

