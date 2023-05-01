namespace Client
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
            this.label1 = new System.Windows.Forms.Label();
            this.servers_listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.edit_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.add_button = new System.Windows.Forms.Button();
            this.IPaddress_textBox = new System.Windows.Forms.TextBox();
            this.serverName_textBox = new System.Windows.Forms.TextBox();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.IPaddress_label = new System.Windows.Forms.Label();
            this.serverName_label = new System.Windows.Forms.Label();
            this.port_label = new System.Windows.Forms.Label();
            this.connect_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(233, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите сервер";
            // 
            // servers_listView
            // 
            this.servers_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.servers_listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.servers_listView.FullRowSelect = true;
            this.servers_listView.GridLines = true;
            this.servers_listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.servers_listView.HideSelection = false;
            this.servers_listView.Location = new System.Drawing.Point(12, 84);
            this.servers_listView.MultiSelect = false;
            this.servers_listView.Name = "servers_listView";
            this.servers_listView.Size = new System.Drawing.Size(627, 192);
            this.servers_listView.TabIndex = 2;
            this.servers_listView.UseCompatibleStateImageBehavior = false;
            this.servers_listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя сервера";
            this.columnHeader1.Width = 187;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP адрес";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Номер порта";
            this.columnHeader3.Width = 131;
            // 
            // edit_button
            // 
            this.edit_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edit_button.Location = new System.Drawing.Point(58, 282);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(112, 34);
            this.edit_button.TabIndex = 3;
            this.edit_button.Text = "Изменить";
            this.edit_button.UseVisualStyleBackColor = true;
            this.edit_button.Click += new System.EventHandler(this.edit_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delete_button.Location = new System.Drawing.Point(189, 282);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(112, 34);
            this.delete_button.TabIndex = 4;
            this.delete_button.Text = "Удалить";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // add_button
            // 
            this.add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_button.Location = new System.Drawing.Point(480, 282);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(112, 34);
            this.add_button.TabIndex = 5;
            this.add_button.Text = "Добавить";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // IPaddress_textBox
            // 
            this.IPaddress_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IPaddress_textBox.Location = new System.Drawing.Point(253, 355);
            this.IPaddress_textBox.Name = "IPaddress_textBox";
            this.IPaddress_textBox.Size = new System.Drawing.Size(119, 24);
            this.IPaddress_textBox.TabIndex = 6;
            this.IPaddress_textBox.Visible = false;
            // 
            // serverName_textBox
            // 
            this.serverName_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serverName_textBox.Location = new System.Drawing.Point(110, 355);
            this.serverName_textBox.Name = "serverName_textBox";
            this.serverName_textBox.Size = new System.Drawing.Size(119, 24);
            this.serverName_textBox.TabIndex = 7;
            this.serverName_textBox.Text = "hfdhfh";
            this.serverName_textBox.Visible = false;
            // 
            // port_textBox
            // 
            this.port_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.port_textBox.Location = new System.Drawing.Point(395, 355);
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(119, 24);
            this.port_textBox.TabIndex = 8;
            this.port_textBox.Visible = false;
            // 
            // IPaddress_label
            // 
            this.IPaddress_label.AutoSize = true;
            this.IPaddress_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IPaddress_label.Location = new System.Drawing.Point(279, 334);
            this.IPaddress_label.Name = "IPaddress_label";
            this.IPaddress_label.Size = new System.Drawing.Size(66, 18);
            this.IPaddress_label.TabIndex = 9;
            this.IPaddress_label.Text = "IP адрес";
            this.IPaddress_label.Visible = false;
            // 
            // serverName_label
            // 
            this.serverName_label.AutoSize = true;
            this.serverName_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serverName_label.Location = new System.Drawing.Point(118, 334);
            this.serverName_label.Name = "serverName_label";
            this.serverName_label.Size = new System.Drawing.Size(98, 18);
            this.serverName_label.TabIndex = 10;
            this.serverName_label.Text = "Имя сервера";
            this.serverName_label.Visible = false;
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.port_label.Location = new System.Drawing.Point(406, 334);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(99, 18);
            this.port_label.TabIndex = 11;
            this.port_label.Text = "Номер порта";
            this.port_label.Visible = false;
            // 
            // connect_button
            // 
            this.connect_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connect_button.Location = new System.Drawing.Point(220, 395);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(192, 60);
            this.connect_button.TabIndex = 12;
            this.connect_button.Text = "Подключиться";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancel_button.Location = new System.Drawing.Point(352, 282);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(112, 34);
            this.cancel_button.TabIndex = 13;
            this.cancel_button.Text = "Отменить";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Visible = false;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 474);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.port_label);
            this.Controls.Add(this.serverName_label);
            this.Controls.Add(this.IPaddress_label);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.serverName_textBox);
            this.Controls.Add(this.IPaddress_textBox);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.edit_button);
            this.Controls.Add(this.servers_listView);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Настройка сервера";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView servers_listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button edit_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.TextBox IPaddress_textBox;
        private System.Windows.Forms.TextBox serverName_textBox;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.Label IPaddress_label;
        private System.Windows.Forms.Label serverName_label;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button cancel_button;
    }
}