namespace IIPU_lab8
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
			this.chbox_Hidden = new System.Windows.Forms.CheckBox();
			this.chbox_EnableHooks = new System.Windows.Forms.CheckBox();
			this.chbox_Logging = new System.Windows.Forms.CheckBox();
			this.tb_Email = new System.Windows.Forms.TextBox();
			this.lb_Email = new System.Windows.Forms.Label();
			this.nud_FileSize = new System.Windows.Forms.NumericUpDown();
			this.lb_FileSize = new System.Windows.Forms.Label();
			this.b_Apply = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nud_FileSize)).BeginInit();
			this.SuspendLayout();
			// 
			// chbox_Hidden
			// 
			this.chbox_Hidden.AutoSize = true;
			this.chbox_Hidden.Location = new System.Drawing.Point(13, 13);
			this.chbox_Hidden.Name = "chbox_Hidden";
			this.chbox_Hidden.Size = new System.Drawing.Size(60, 17);
			this.chbox_Hidden.TabIndex = 0;
			this.chbox_Hidden.Text = "Hidden";
			this.chbox_Hidden.UseVisualStyleBackColor = true;
			// 
			// chbox_EnableHooks
			// 
			this.chbox_EnableHooks.AutoSize = true;
			this.chbox_EnableHooks.Location = new System.Drawing.Point(13, 37);
			this.chbox_EnableHooks.Name = "chbox_EnableHooks";
			this.chbox_EnableHooks.Size = new System.Drawing.Size(91, 17);
			this.chbox_EnableHooks.TabIndex = 1;
			this.chbox_EnableHooks.Text = "Enable hooks";
			this.chbox_EnableHooks.UseVisualStyleBackColor = true;
			// 
			// chbox_Logging
			// 
			this.chbox_Logging.AutoSize = true;
			this.chbox_Logging.Location = new System.Drawing.Point(13, 61);
			this.chbox_Logging.Name = "chbox_Logging";
			this.chbox_Logging.Size = new System.Drawing.Size(64, 17);
			this.chbox_Logging.TabIndex = 2;
			this.chbox_Logging.Text = "Logging";
			this.chbox_Logging.UseVisualStyleBackColor = true;
			// 
			// tb_Email
			// 
			this.tb_Email.Location = new System.Drawing.Point(112, 34);
			this.tb_Email.Name = "tb_Email";
			this.tb_Email.Size = new System.Drawing.Size(160, 20);
			this.tb_Email.TabIndex = 3;
			// 
			// lb_Email
			// 
			this.lb_Email.AutoSize = true;
			this.lb_Email.Location = new System.Drawing.Point(109, 13);
			this.lb_Email.Name = "lb_Email";
			this.lb_Email.Size = new System.Drawing.Size(32, 13);
			this.lb_Email.TabIndex = 4;
			this.lb_Email.Text = "Email";
			// 
			// nud_FileSize
			// 
			this.nud_FileSize.Location = new System.Drawing.Point(112, 86);
			this.nud_FileSize.Name = "nud_FileSize";
			this.nud_FileSize.Size = new System.Drawing.Size(160, 20);
			this.nud_FileSize.TabIndex = 5;
			// 
			// lb_FileSize
			// 
			this.lb_FileSize.AutoSize = true;
			this.lb_FileSize.Location = new System.Drawing.Point(112, 61);
			this.lb_FileSize.Name = "lb_FileSize";
			this.lb_FileSize.Size = new System.Drawing.Size(46, 13);
			this.lb_FileSize.TabIndex = 6;
			this.lb_FileSize.Text = "File Size";
			// 
			// b_Apply
			// 
			this.b_Apply.Location = new System.Drawing.Point(13, 83);
			this.b_Apply.Name = "b_Apply";
			this.b_Apply.Size = new System.Drawing.Size(75, 23);
			this.b_Apply.TabIndex = 7;
			this.b_Apply.Text = "Apply";
			this.b_Apply.UseVisualStyleBackColor = true;
			this.b_Apply.Click += new System.EventHandler(this.b_Apply_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 122);
			this.Controls.Add(this.b_Apply);
			this.Controls.Add(this.lb_FileSize);
			this.Controls.Add(this.nud_FileSize);
			this.Controls.Add(this.lb_Email);
			this.Controls.Add(this.tb_Email);
			this.Controls.Add(this.chbox_Logging);
			this.Controls.Add(this.chbox_EnableHooks);
			this.Controls.Add(this.chbox_Hidden);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.nud_FileSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lb_Email;
		private System.Windows.Forms.Label lb_FileSize;
		public System.Windows.Forms.CheckBox chbox_Hidden;
		public System.Windows.Forms.CheckBox chbox_EnableHooks;
		public System.Windows.Forms.CheckBox chbox_Logging;
		public System.Windows.Forms.TextBox tb_Email;
		public System.Windows.Forms.NumericUpDown nud_FileSize;
		public System.Windows.Forms.Button b_Apply;
	}
}

