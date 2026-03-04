namespace JobHunter
{
    partial class Form1
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
            button_IlanEkle = new Button();
            textBox1 = new TextBox();
            btn_Pdf_Upload = new Button();
            label1 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button_IlanEkle
            // 
            button_IlanEkle.Location = new Point(317, 150);
            button_IlanEkle.Name = "button_IlanEkle";
            button_IlanEkle.Size = new Size(75, 23);
            button_IlanEkle.TabIndex = 0;
            button_IlanEkle.Text = "İlan Bul";
            button_IlanEkle.UseVisualStyleBackColor = true;
            button_IlanEkle.Click += button_IlanEkle_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(342, 121);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(241, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += txtKeyword;
            // 
            // btn_Pdf_Upload
            // 
            btn_Pdf_Upload.Location = new Point(413, 150);
            btn_Pdf_Upload.Name = "btn_Pdf_Upload";
            btn_Pdf_Upload.Size = new Size(75, 23);
            btn_Pdf_Upload.TabIndex = 2;
            btn_Pdf_Upload.Text = "Cv Yükle";
            btn_Pdf_Upload.UseVisualStyleBackColor = true;
            btn_Pdf_Upload.Click += btn_Pdf_Upload_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(505, 154);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(186, 121);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 23);
            textBox2.TabIndex = 4;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(240, 103);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 5;
            label2.Text = "Şehir";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(434, 95);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 6;
            label3.Text = "Aranan İş";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(btn_Pdf_Upload);
            Controls.Add(textBox1);
            Controls.Add(button_IlanEkle);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_IlanEkle;
        private TextBox textBox1;
        private Button btn_Pdf_Upload;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
    }
}