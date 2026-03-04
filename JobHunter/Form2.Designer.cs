namespace JobHunter
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            dgvJobs = new DataGridView();
            btn_SendMail = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label2 = new Label();
            btn_ApplyAll = new Button();
            btn_save = new Button();
            textBox3 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBox4 = new TextBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvJobs).BeginInit();
            SuspendLayout();
            // 
            // dgvJobs
            // 
            dgvJobs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvJobs.Location = new Point(0, 0);
            dgvJobs.Name = "dgvJobs";
            dgvJobs.Size = new Size(969, 301);
            dgvJobs.TabIndex = 0;
            dgvJobs.CellContentClick += dgvJobs_CellContentClick;
            // 
            // btn_SendMail
            // 
            btn_SendMail.Location = new Point(12, 310);
            btn_SendMail.Name = "btn_SendMail";
            btn_SendMail.Size = new Size(116, 23);
            btn_SendMail.TabIndex = 1;
            btn_SendMail.Text = "Başvur";
            btn_SendMail.UseVisualStyleBackColor = true;
            btn_SendMail.Click += btn_SendMail_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(791, 310);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 3;
            label1.Text = "İsim";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(828, 307);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(111, 23);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(716, 389);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(223, 76);
            textBox2.TabIndex = 5;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(651, 392);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 6;
            label2.Text = "ÖnYazı";
            // 
            // btn_ApplyAll
            // 
            btn_ApplyAll.Location = new Point(174, 310);
            btn_ApplyAll.Name = "btn_ApplyAll";
            btn_ApplyAll.Size = new Size(115, 23);
            btn_ApplyAll.TabIndex = 7;
            btn_ApplyAll.Text = "HepsineBaşvur";
            btn_ApplyAll.UseVisualStyleBackColor = true;
            btn_ApplyAll.Click += button1_ClickAsync;
            // 
            // btn_save
            // 
            btn_save.Location = new Point(806, 471);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(133, 23);
            btn_save.TabIndex = 8;
            btn_save.Text = "Bilgileri Kaydet";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(828, 333);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(111, 23);
            textBox3.TabIndex = 9;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(778, 333);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 10;
            label3.Text = "Eposta";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(778, 360);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 11;
            label4.Text = "Şifre";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(828, 360);
            textBox4.Name = "textBox4";
            textBox4.PasswordChar = '*';
            textBox4.Size = new Size(111, 23);
            textBox4.TabIndex = 12;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label6.Location = new Point(574, 497);
            label6.Name = "label6";
            label6.Size = new Size(395, 225);
            label6.TabIndex = 14;
            label6.Text = resources.GetString("label6.Text");
            label6.Click += label6_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(969, 705);
            Controls.Add(label6);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(btn_save);
            Controls.Add(btn_ApplyAll);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(btn_SendMail);
            Controls.Add(dgvJobs);
            Name = "Form2";
            Text = "Form2";
            FormClosing += Form2_FormClosing;
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dgvJobs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvJobs;
        private Button btn_SendMail;
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private Button btn_ApplyAll;
        private Button btn_save;
        private TextBox textBox3;
        private Label label3;
        private Label label4;
        private TextBox textBox4;
        private Label label6;
    }
}