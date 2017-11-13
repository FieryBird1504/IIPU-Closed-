namespace IIPU_lab4_GUI
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
			this.cb_List = new System.Windows.Forms.ComboBox();
			this.b_eject = new System.Windows.Forms.Button();
			this.lb_info = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cb_List
			// 
			this.cb_List.FormattingEnabled = true;
			this.cb_List.Location = new System.Drawing.Point(12, 12);
			this.cb_List.Name = "cb_List";
			this.cb_List.Size = new System.Drawing.Size(121, 21);
			this.cb_List.TabIndex = 0;
			this.cb_List.Text = "<none>";
			this.cb_List.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// b_eject
			// 
			this.b_eject.Location = new System.Drawing.Point(139, 12);
			this.b_eject.Name = "b_eject";
			this.b_eject.Size = new System.Drawing.Size(75, 23);
			this.b_eject.TabIndex = 1;
			this.b_eject.Text = "Disable";
			this.b_eject.UseVisualStyleBackColor = true;
			this.b_eject.Click += new System.EventHandler(this.button1_Click);
			// 
			// lb_info
			// 
			this.lb_info.AutoSize = true;
			this.lb_info.Location = new System.Drawing.Point(145, 50);
			this.lb_info.Name = "lb_info";
			this.lb_info.Size = new System.Drawing.Size(35, 13);
			this.lb_info.TabIndex = 2;
			this.lb_info.Text = "label1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 159);
			this.Controls.Add(this.lb_info);
			this.Controls.Add(this.b_eject);
			this.Controls.Add(this.cb_List);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button b_eject;
		public System.Windows.Forms.ComboBox cb_List;
		public System.Windows.Forms.Label lb_info;
	}
}

