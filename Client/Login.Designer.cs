namespace UGN_Client
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.reader = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AUTO = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Username1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.UID = new System.Windows.Forms.Label();
            this.GID = new System.Windows.Forms.Label();
            this.ONLINE = new System.Windows.Forms.Label();
            this.READ = new System.Windows.Forms.Label();
            this.TOKEN = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ugnCheckBox1 = new UGNCheckBox();
            this.ugN_Button1 = new UGN_Button();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // reader
            // 
            resources.ApplyResources(this.reader, "reader");
            this.reader.Name = "reader";
            // 
            // Password
            // 
            this.Password.AcceptsReturn = true;
            this.Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.Password, "Password");
            this.Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.Password.Name = "Password";
            this.Password.UseSystemPasswordChar = true;
            this.Password.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            this.Password.MouseEnter += new System.EventHandler(this.Password_MouseEnter);
            this.Password.MouseLeave += new System.EventHandler(this.Password_MouseLeave);
            this.Password.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox2_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.label1.Name = "label1";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.label2.Name = "label2";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::UGN_Client.Properties.Resources.Bar;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = global::UGN_Client.Properties.Resources.ugn_logo_32;
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Name = "panel8";
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // panel10
            // 
            this.panel10.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.panel10, "panel10");
            this.panel10.Name = "panel10";
            this.panel10.Click += new System.EventHandler(this.panel10_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.label10.Name = "label10";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(187)))));
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.Password);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.panel3.Name = "panel3";
            // 
            // Username1
            // 
            this.Username1.AcceptsReturn = true;
            this.Username1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.Username1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.Username1, "Username1");
            this.Username1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.Username1.Name = "Username1";
            this.Username1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.Username1.MouseEnter += new System.EventHandler(this.Username1_MouseEnter);
            this.Username1.MouseLeave += new System.EventHandler(this.Username1_MouseLeave);
            this.Username1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(187)))));
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.panel2.Name = "panel2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // UID
            // 
            resources.ApplyResources(this.UID, "UID");
            this.UID.Name = "UID";
            // 
            // GID
            // 
            resources.ApplyResources(this.GID, "GID");
            this.GID.Name = "GID";
            // 
            // ONLINE
            // 
            resources.ApplyResources(this.ONLINE, "ONLINE");
            this.ONLINE.Name = "ONLINE";
            // 
            // READ
            // 
            resources.ApplyResources(this.READ, "READ");
            this.READ.Name = "READ";
            // 
            // TOKEN
            // 
            resources.ApplyResources(this.TOKEN, "TOKEN");
            this.TOKEN.Name = "TOKEN";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.BackgroundImage = global::UGN_Client.Properties.Resources.logo;
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.TOKEN);
            this.panel5.Controls.Add(this.READ);
            this.panel5.Controls.Add(this.ONLINE);
            this.panel5.Controls.Add(this.GID);
            this.panel5.Controls.Add(this.UID);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Name = "panel5";
            // 
            // ugnCheckBox1
            // 
            this.ugnCheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.ugnCheckBox1.Checked = false;
            this.ugnCheckBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.ugnCheckBox1, "ugnCheckBox1");
            this.ugnCheckBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            this.ugnCheckBox1.Name = "ugnCheckBox1";
            this.ugnCheckBox1.CheckedChanged += new UGNCheckBox.CheckedChangedEventHandler(this.ugnCheckBox1_CheckedChanged);
            // 
            // ugN_Button1
            // 
            this.ugN_Button1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.ugN_Button1, "ugN_Button1");
            this.ugN_Button1.Image = null;
            this.ugN_Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ugN_Button1.Name = "ugN_Button1";
            this.ugN_Button1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ugN_Button1.Click += new System.EventHandler(this.ugN_Button1_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.Username1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ugnCheckBox1);
            this.Controls.Add(this.ugN_Button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.reader);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox reader;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer AUTO;
        private System.Windows.Forms.Panel panel1;
        private UGN_Button ugN_Button1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private UGNCheckBox ugnCheckBox1;
        public System.Windows.Forms.TextBox Username1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label UID;
        private System.Windows.Forms.Label GID;
        private System.Windows.Forms.Label ONLINE;
        private System.Windows.Forms.Label READ;
        private System.Windows.Forms.Label TOKEN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;

    }
}
