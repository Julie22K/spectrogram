namespace IIS_test_2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            stop = new Button();
            start = new Button();
            Record = new GroupBox();
            Listen = new GroupBox();
            comboBox1 = new ComboBox();
            res = new Label();
            Record.SuspendLayout();
            Listen.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(6, 51);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Записати";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(87, 51);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Зберегти";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 22);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Назва файлу";
            textBox1.Size = new Size(156, 23);
            textBox1.TabIndex = 2;
            // 
            // stop
            // 
            stop.Location = new Point(94, 51);
            stop.Name = "stop";
            stop.Size = new Size(75, 23);
            stop.TabIndex = 3;
            stop.Text = "Зупинити";
            stop.UseVisualStyleBackColor = true;
            stop.Click += stop_Click;
            // 
            // start
            // 
            start.Location = new Point(6, 51);
            start.Name = "start";
            start.Size = new Size(82, 23);
            start.TabIndex = 4;
            start.Text = "Почати";
            start.UseVisualStyleBackColor = true;
            start.Click += start_Click;
            // 
            // Record
            // 
            Record.Controls.Add(textBox1);
            Record.Controls.Add(button1);
            Record.Controls.Add(button2);
            Record.Location = new Point(12, 12);
            Record.Name = "Record";
            Record.Size = new Size(175, 88);
            Record.TabIndex = 5;
            Record.TabStop = false;
            Record.Text = "Записати файл";
            // 
            // Listen
            // 
            Listen.Controls.Add(comboBox1);
            Listen.Controls.Add(start);
            Listen.Controls.Add(stop);
            Listen.Location = new Point(193, 12);
            Listen.Name = "Listen";
            Listen.Size = new Size(175, 88);
            Listen.TabIndex = 6;
            Listen.TabStop = false;
            Listen.Text = "Прослухати файл";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(8, 22);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(161, 23);
            comboBox1.TabIndex = 6;
            // 
            // res
            // 
            res.AutoSize = true;
            res.Location = new Point(12, 109);
            res.Name = "res";
            res.Size = new Size(38, 15);
            res.TabIndex = 7;
            res.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 437);
            Controls.Add(res);
            Controls.Add(Listen);
            Controls.Add(Record);
            Name = "Form1";
            Text = "Form1";
            Record.ResumeLayout(false);
            Record.PerformLayout();
            Listen.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private Button stop;
        private Button start;
        private GroupBox Record;
        private GroupBox Listen;
        private ComboBox comboBox1;
        private Label res;
    }
}