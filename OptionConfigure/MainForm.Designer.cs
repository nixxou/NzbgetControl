namespace OptionConfigure
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.radio_ssl_false = new System.Windows.Forms.RadioButton();
            this.radio_ssl_true = new System.Windows.Forms.RadioButton();
            this.numericUpDown_connections = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_port = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_host = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_submit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_Nzbget_false = new System.Windows.Forms.Label();
            this.buttonInstallNzbGet = new System.Windows.Forms.Button();
            this.label_Nzbget_true = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_Imdisk_false = new System.Windows.Forms.Label();
            this.buttonInstallImDisk = new System.Windows.Forms.Button();
            this.label_Imdisk_true = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.radio_cleanram_false = new System.Windows.Forms.RadioButton();
            this.radio_cleanram_true = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.radio_rootextract_false = new System.Windows.Forms.RadioButton();
            this.radio_rootextract_true = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.radio_extract_false = new System.Windows.Forms.RadioButton();
            this.radio_extract_true = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_connections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.numericUpDown_connections);
            this.groupBox1.Controls.Add(this.numericUpDown_port);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_password);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_user);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_host);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(391, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 189);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "News Server Config";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.radio_ssl_false);
            this.panel6.Controls.Add(this.radio_ssl_true);
            this.panel6.Location = new System.Drawing.Point(6, 154);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(204, 23);
            this.panel6.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "SSL :";
            // 
            // radio_ssl_false
            // 
            this.radio_ssl_false.AutoSize = true;
            this.radio_ssl_false.Location = new System.Drawing.Point(136, 4);
            this.radio_ssl_false.Name = "radio_ssl_false";
            this.radio_ssl_false.Size = new System.Drawing.Size(66, 17);
            this.radio_ssl_false.TabIndex = 27;
            this.radio_ssl_false.TabStop = true;
            this.radio_ssl_false.Text = "Disabled";
            this.radio_ssl_false.UseVisualStyleBackColor = true;
            // 
            // radio_ssl_true
            // 
            this.radio_ssl_true.AutoSize = true;
            this.radio_ssl_true.Location = new System.Drawing.Point(70, 4);
            this.radio_ssl_true.Name = "radio_ssl_true";
            this.radio_ssl_true.Size = new System.Drawing.Size(64, 17);
            this.radio_ssl_true.TabIndex = 26;
            this.radio_ssl_true.TabStop = true;
            this.radio_ssl_true.Text = "Enabled";
            this.radio_ssl_true.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_connections
            // 
            this.numericUpDown_connections.Location = new System.Drawing.Point(78, 128);
            this.numericUpDown_connections.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_connections.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_connections.Name = "numericUpDown_connections";
            this.numericUpDown_connections.Size = new System.Drawing.Size(130, 20);
            this.numericUpDown_connections.TabIndex = 29;
            this.numericUpDown_connections.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_port
            // 
            this.numericUpDown_port.Location = new System.Drawing.Point(78, 50);
            this.numericUpDown_port.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_port.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_port.Name = "numericUpDown_port";
            this.numericUpDown_port.Size = new System.Drawing.Size(130, 20);
            this.numericUpDown_port.TabIndex = 28;
            this.numericUpDown_port.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Connections";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(78, 102);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(132, 20);
            this.textBox_password.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Password";
            // 
            // textBox_user
            // 
            this.textBox_user.Location = new System.Drawing.Point(78, 76);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.Size = new System.Drawing.Size(132, 20);
            this.textBox_user.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Port";
            // 
            // textBox_host
            // 
            this.textBox_host.Location = new System.Drawing.Point(78, 24);
            this.textBox_host.Name = "textBox_host";
            this.textBox_host.Size = new System.Drawing.Size(132, 20);
            this.textBox_host.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Host";
            // 
            // button_submit
            // 
            this.button_submit.Location = new System.Drawing.Point(751, 159);
            this.button_submit.Name = "button_submit";
            this.button_submit.Size = new System.Drawing.Size(123, 32);
            this.button_submit.TabIndex = 27;
            this.button_submit.Text = "Submit Config";
            this.button_submit.UseVisualStyleBackColor = true;
            this.button_submit.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_Nzbget_false);
            this.groupBox2.Controls.Add(this.buttonInstallNzbGet);
            this.groupBox2.Controls.Add(this.label_Nzbget_true);
            this.groupBox2.Controls.Add(this.linkLabel2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(8, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 96);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NzbGet";
            // 
            // label_Nzbget_false
            // 
            this.label_Nzbget_false.AutoSize = true;
            this.label_Nzbget_false.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Nzbget_false.ForeColor = System.Drawing.Color.Red;
            this.label_Nzbget_false.Location = new System.Drawing.Point(166, 22);
            this.label_Nzbget_false.Name = "label_Nzbget_false";
            this.label_Nzbget_false.Size = new System.Drawing.Size(125, 24);
            this.label_Nzbget_false.TabIndex = 21;
            this.label_Nzbget_false.Text = "Not Installed";
            // 
            // buttonInstallNzbGet
            // 
            this.buttonInstallNzbGet.Location = new System.Drawing.Point(293, 62);
            this.buttonInstallNzbGet.Name = "buttonInstallNzbGet";
            this.buttonInstallNzbGet.Size = new System.Drawing.Size(78, 26);
            this.buttonInstallNzbGet.TabIndex = 20;
            this.buttonInstallNzbGet.Text = "Install";
            this.buttonInstallNzbGet.UseVisualStyleBackColor = true;
            this.buttonInstallNzbGet.Click += new System.EventHandler(this.buttonInstallNzbGet_Click);
            // 
            // label_Nzbget_true
            // 
            this.label_Nzbget_true.AutoSize = true;
            this.label_Nzbget_true.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Nzbget_true.ForeColor = System.Drawing.Color.Green;
            this.label_Nzbget_true.Location = new System.Drawing.Point(166, 22);
            this.label_Nzbget_true.Name = "label_Nzbget_true";
            this.label_Nzbget_true.Size = new System.Drawing.Size(87, 24);
            this.label_Nzbget_true.TabIndex = 19;
            this.label_Nzbget_true.Text = "Installed";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(7, 69);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(144, 13);
            this.linkLabel2.TabIndex = 18;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "https://nzbget.net/download";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "NZBGet Status :";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_Imdisk_false);
            this.groupBox3.Controls.Add(this.buttonInstallImDisk);
            this.groupBox3.Controls.Add(this.label_Imdisk_true);
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(8, 111);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(377, 88);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ImDisk";
            // 
            // label_Imdisk_false
            // 
            this.label_Imdisk_false.AutoSize = true;
            this.label_Imdisk_false.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Imdisk_false.ForeColor = System.Drawing.Color.Red;
            this.label_Imdisk_false.Location = new System.Drawing.Point(166, 15);
            this.label_Imdisk_false.Name = "label_Imdisk_false";
            this.label_Imdisk_false.Size = new System.Drawing.Size(125, 24);
            this.label_Imdisk_false.TabIndex = 22;
            this.label_Imdisk_false.Text = "Not Installed";
            // 
            // buttonInstallImDisk
            // 
            this.buttonInstallImDisk.Location = new System.Drawing.Point(293, 48);
            this.buttonInstallImDisk.Name = "buttonInstallImDisk";
            this.buttonInstallImDisk.Size = new System.Drawing.Size(78, 26);
            this.buttonInstallImDisk.TabIndex = 21;
            this.buttonInstallImDisk.Text = "Install";
            this.buttonInstallImDisk.UseVisualStyleBackColor = true;
            this.buttonInstallImDisk.Click += new System.EventHandler(this.buttonInstallImDisk_Click);
            // 
            // label_Imdisk_true
            // 
            this.label_Imdisk_true.AutoSize = true;
            this.label_Imdisk_true.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Imdisk_true.ForeColor = System.Drawing.Color.Green;
            this.label_Imdisk_true.Location = new System.Drawing.Point(166, 15);
            this.label_Imdisk_true.Name = "label_Imdisk_true";
            this.label_Imdisk_true.Size = new System.Drawing.Size(87, 24);
            this.label_Imdisk_true.TabIndex = 20;
            this.label_Imdisk_true.Text = "Installed";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(7, 55);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(230, 13);
            this.linkLabel1.TabIndex = 19;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.ltr-data.se/opencode.html/#ImDisk";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "ImDisk Status :";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel4);
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Location = new System.Drawing.Point(620, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(254, 105);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Default Options";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.radio_cleanram_false);
            this.panel4.Controls.Add(this.radio_cleanram_true);
            this.panel4.Location = new System.Drawing.Point(6, 69);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(228, 23);
            this.panel4.TabIndex = 35;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Cleanram :";
            // 
            // radio_cleanram_false
            // 
            this.radio_cleanram_false.AutoSize = true;
            this.radio_cleanram_false.Location = new System.Drawing.Point(159, 4);
            this.radio_cleanram_false.Name = "radio_cleanram_false";
            this.radio_cleanram_false.Size = new System.Drawing.Size(66, 17);
            this.radio_cleanram_false.TabIndex = 27;
            this.radio_cleanram_false.TabStop = true;
            this.radio_cleanram_false.Text = "Disabled";
            this.radio_cleanram_false.UseVisualStyleBackColor = true;
            // 
            // radio_cleanram_true
            // 
            this.radio_cleanram_true.AutoSize = true;
            this.radio_cleanram_true.Location = new System.Drawing.Point(89, 4);
            this.radio_cleanram_true.Name = "radio_cleanram_true";
            this.radio_cleanram_true.Size = new System.Drawing.Size(64, 17);
            this.radio_cleanram_true.TabIndex = 26;
            this.radio_cleanram_true.TabStop = true;
            this.radio_cleanram_true.Text = "Enabled";
            this.radio_cleanram_true.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.radio_rootextract_false);
            this.panel2.Controls.Add(this.radio_rootextract_true);
            this.panel2.Location = new System.Drawing.Point(6, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(228, 23);
            this.panel2.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "RootExtract :";
            // 
            // radio_rootextract_false
            // 
            this.radio_rootextract_false.AutoSize = true;
            this.radio_rootextract_false.Location = new System.Drawing.Point(159, 4);
            this.radio_rootextract_false.Name = "radio_rootextract_false";
            this.radio_rootextract_false.Size = new System.Drawing.Size(66, 17);
            this.radio_rootextract_false.TabIndex = 27;
            this.radio_rootextract_false.TabStop = true;
            this.radio_rootextract_false.Text = "Disabled";
            this.radio_rootextract_false.UseVisualStyleBackColor = true;
            // 
            // radio_rootextract_true
            // 
            this.radio_rootextract_true.AutoSize = true;
            this.radio_rootextract_true.Location = new System.Drawing.Point(89, 4);
            this.radio_rootextract_true.Name = "radio_rootextract_true";
            this.radio_rootextract_true.Size = new System.Drawing.Size(64, 17);
            this.radio_rootextract_true.TabIndex = 26;
            this.radio_rootextract_true.TabStop = true;
            this.radio_rootextract_true.Text = "Enabled";
            this.radio_rootextract_true.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.radio_extract_false);
            this.panel1.Controls.Add(this.radio_extract_true);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 23);
            this.panel1.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Extract :";
            // 
            // radio_extract_false
            // 
            this.radio_extract_false.AutoSize = true;
            this.radio_extract_false.Location = new System.Drawing.Point(159, 4);
            this.radio_extract_false.Name = "radio_extract_false";
            this.radio_extract_false.Size = new System.Drawing.Size(66, 17);
            this.radio_extract_false.TabIndex = 27;
            this.radio_extract_false.TabStop = true;
            this.radio_extract_false.Text = "Disabled";
            this.radio_extract_false.UseVisualStyleBackColor = true;
            // 
            // radio_extract_true
            // 
            this.radio_extract_true.AutoSize = true;
            this.radio_extract_true.Location = new System.Drawing.Point(89, 4);
            this.radio_extract_true.Name = "radio_extract_true";
            this.radio_extract_true.Size = new System.Drawing.Size(64, 17);
            this.radio_extract_true.TabIndex = 26;
            this.radio_extract_true.TabStop = true;
            this.radio_extract_true.Text = "Enabled";
            this.radio_extract_true.UseVisualStyleBackColor = true;
            this.radio_extract_true.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(620, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 32);
            this.button1.TabIndex = 28;
            this.button1.Text = "Test Server Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 202);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button_submit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_connections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_submit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_host;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button buttonInstallNzbGet;
        private System.Windows.Forms.Label label_Nzbget_true;
        private System.Windows.Forms.Button buttonInstallImDisk;
        private System.Windows.Forms.Label label_Imdisk_true;
        private System.Windows.Forms.NumericUpDown numericUpDown_connections;
        private System.Windows.Forms.NumericUpDown numericUpDown_port;
        private System.Windows.Forms.Label label_Nzbget_false;
        private System.Windows.Forms.Label label_Imdisk_false;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radio_extract_false;
        private System.Windows.Forms.RadioButton radio_extract_true;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radio_ssl_false;
        private System.Windows.Forms.RadioButton radio_ssl_true;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radio_cleanram_false;
        private System.Windows.Forms.RadioButton radio_cleanram_true;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radio_rootextract_false;
        private System.Windows.Forms.RadioButton radio_rootextract_true;
        private System.Windows.Forms.Button button1;
    }
}