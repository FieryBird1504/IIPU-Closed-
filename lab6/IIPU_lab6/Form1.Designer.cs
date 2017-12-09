namespace IIPU_lab6
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
            this.lbox_wifiList = new System.Windows.Forms.ListBox();
            this.lb_info = new System.Windows.Forms.Label();
            this.b_connect = new System.Windows.Forms.Button();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbox_wifiList
            // 
            this.lbox_wifiList.FormattingEnabled = true;
            this.lbox_wifiList.ItemHeight = 16;
            this.lbox_wifiList.Location = new System.Drawing.Point(17, 16);
            this.lbox_wifiList.Margin = new System.Windows.Forms.Padding(4);
            this.lbox_wifiList.Name = "lbox_wifiList";
            this.lbox_wifiList.Size = new System.Drawing.Size(390, 180);
            this.lbox_wifiList.TabIndex = 0;
            // 
            // lb_info
            // 
            this.lb_info.AutoSize = true;
            this.lb_info.Location = new System.Drawing.Point(14, 212);
            this.lb_info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(0, 17);
            this.lb_info.TabIndex = 1;
            this.lb_info.Click += new System.EventHandler(this.label1_Click);
            // 
            // b_connect
            // 
            this.b_connect.Location = new System.Drawing.Point(307, 322);
            this.b_connect.Margin = new System.Windows.Forms.Padding(4);
            this.b_connect.Name = "b_connect";
            this.b_connect.Size = new System.Drawing.Size(100, 28);
            this.b_connect.TabIndex = 2;
            this.b_connect.Text = "Connect";
            this.b_connect.UseVisualStyleBackColor = true;
            this.b_connect.Click += new System.EventHandler(this.b_connect_Click);
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(237, 292);
            this.tb_password.Margin = new System.Windows.Forms.Padding(4);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(170, 22);
            this.tb_password.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 363);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.b_connect);
            this.Controls.Add(this.lb_info);
            this.Controls.Add(this.lbox_wifiList);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.ListBox lbox_wifiList;
		public System.Windows.Forms.Label lb_info;
		public System.Windows.Forms.Button b_connect;
		private System.Windows.Forms.TextBox tb_password;

	}
}

